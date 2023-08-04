using EpamTestApi.Config;
using EpamTestApi.Model;
using EpamTestApi.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace EpamTestApi.StepDefinitions
{
    [Binding]
    public sealed class SocialMediaSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RestHelper _restHelper;

        public SocialMediaSteps(ScenarioContext scenarioContext, RestHelper restHelper) {
            _scenarioContext = scenarioContext;
            _restHelper = restHelper;
        }   

        [When(@"User requests for all the available Posts")]
        public async Task WhenUserGetsAllTheAvailablePosts()
        {        
            RestRequest restRequest = new RestRequest("posts");
            var restResponse = await _restHelper.RestClient.GetAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
        }

        [Then(@"Verify user gets all the posts")]
        public void ThenVerifyUserGetsAllThePosts()
        {
           RestResponse response =_scenarioContext.Get<RestResponse>("restResponse");
           response.Should().NotBeNull();
           response.StatusCode.Should().Be(HttpStatusCode.OK);
           response.Content.Should().NotBeNull();
           response.IsSuccessful.Should().BeTrue();
           List<MediaPost> ListOfMediaPost = JsonConvert.DeserializeObject<List<MediaPost>>(response.Content);
           ListOfMediaPost.Should().NotBeNullOrEmpty();
           ListOfMediaPost.Count.Should().Be(100);
        }

        [When(@"User requests for media post by Id = (.*)")]
        public async Task WhenUserRequestsForMediaPostById(int id)
        {           
            RestRequest restRequest = new RestRequest($"posts/{id}");
            var restResponse = await _restHelper.RestClient.GetAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
            _scenarioContext.Set(id, "Id");
        }

        [Then(@"Verify user gets requested post")]
        public void ThenVerifyUserGetsRequestedPost()
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            MediaPost mediaPost = JsonConvert.DeserializeObject<MediaPost>(response.Content);
            mediaPost.Should().NotBeNull();
            mediaPost.Id.Should().Be(_scenarioContext.Get<int>("Id"));
        }


        [When(@"User creates new post")]
        public async Task WhenUserCreatesNewPost()
        {
            var post = CommonMethods.GetTestData<MediaPost>();
            RestRequest restRequest = new RestRequest("posts").AddJsonBody(post);
            
            var restResponse = await _restHelper.RestClient.PostAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
                      
        }

        [Then(@"Verify new post has been created succesfully\.")]
        public void ThenVerifyNewPostHasBeenCreatedSuccesfully_()
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            MediaPost mediaPost = JsonConvert.DeserializeObject<MediaPost>(response.Content);
            mediaPost.Should().NotBeNull();
            mediaPost.Id.Should().BeGreaterThan(0);
            mediaPost.Title.Should().Be("foo");
            mediaPost.Body.Should().Be("bar");
        }

        [When(@"User updates media post for Id = (.*)")]
        public async Task WhenUserUpdatesMediaPostForId(int id)
        {           
            RestRequest restRequest = new RestRequest($"posts/{id}");
            restRequest.AddJsonBody<MediaPost>(GetUpdatedRequest(id));
            var restResponse = await _restHelper.RestClient.PutAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
            _scenarioContext.Set(id, "Id");
        }

        [Then(@"Verify media post updated succesfully for id = (.*)")]
        public void WhenVerifyMediaPostUpdatedSuccesfullyForId(int id)
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            MediaPost mediaPost = JsonConvert.DeserializeObject<MediaPost>(response.Content);
            mediaPost.Should().NotBeNull();
            mediaPost.Id.Should().Be(id);
            mediaPost.Title.Should().Be("Updated foo");
            mediaPost.Body.Should().Be("Updated bar");
        }

        [When(@"User updates title of post for Id = (.*)")]
        public async Task WhenUserUpdatesTitleOfPostForId(int id)
        {          
            RestRequest restRequest = new RestRequest($"posts/{id}");
            restRequest.AddJsonBody("{\"title\": \"patched foo\"}");
            var restResponse = await _restHelper.RestClient.PatchAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
            _scenarioContext.Set(id, "Id");
        }

        [Then(@"Verify media post title updated succesfully for id = (.*)")]
        public void WhenVerifyMediaPostTitleUpdatedSuccesfullyForId(int id)
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            MediaPost mediaPost = JsonConvert.DeserializeObject<MediaPost>(response.Content);
            mediaPost.Should().NotBeNull();
            mediaPost.Id.Should().Be(id);
            mediaPost.Title.Should().Be("patched foo");         
        }


        [When(@"User deletes media post for id = (.*)")]
        public async Task WhenUserDeletesMediaPostForId(int id)
        {
            RestRequest restRequest = new RestRequest($"posts/{id}");           
            var restResponse = await _restHelper.RestClient.DeleteAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
        }

        [Then(@"Verify media post is deleted succesfully")]
        public void WhenVerifyMediaPostIsDeletedSuccesfully()
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            MediaPost mediaPost = JsonConvert.DeserializeObject<MediaPost>(response.Content);
            mediaPost.Should().NotBeNull();
            mediaPost.Id.Should().Be(0);
            mediaPost.Body.Should().BeNull();
            mediaPost.Title.Should().BeNull();
            mediaPost.UserId.Should().Be(0);
        }


        [When(@"User requests all the comments for post id (.*)")]
        public async Task WhenUserRequestsAllTheCommentsForPostId(int id)
        {          
            RestRequest restRequest = new RestRequest($"posts/{id}/comments");
            var restResponse = await _restHelper.RestClient.GetAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
            _scenarioContext.Set(id, "Id");
        }

        [Then(@"Verify the response returns requested comments")]
        public void ThenVerifyTheResponseReturnsRequestedComments()
        {
            RestResponse response = _scenarioContext.Get<RestResponse>("restResponse");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.IsSuccessful.Should().BeTrue();
            List<Comment> ListOfComments = JsonConvert.DeserializeObject<List<Comment>>(response.Content);
            ListOfComments.Should().NotBeNullOrEmpty();
            ListOfComments.All(x => x.PostId == _scenarioContext.Get<int>("Id")).Should().BeTrue();
        }

        [When(@"User requests all the comments by query parameter for post id (.*)")]
        public async Task WhenUserRequestsAllTheCommentsByQueryParameterForPostId(int id)
        {
         
            RestRequest restRequest = new RestRequest($"comments");
            restRequest.AddQueryParameter("postId", id.ToString());

            var restResponse = await _restHelper.RestClient.GetAsync(restRequest);
            _scenarioContext.Set(restResponse, "restResponse");
            _scenarioContext.Set(id, "Id");
        }

        private MediaPost GetUpdatedRequest(int id)
        {
            return new MediaPost { UserId=1, Id = id, Title = "Updated foo", Body = "Updated bar" };
        }
    }
}