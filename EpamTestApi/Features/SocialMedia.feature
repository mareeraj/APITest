Feature: Social Media Features
Test all the social media sites features

@read
Scenario: Verify user is able to get all the posts
	When User requests for all the available Posts
	Then Verify user gets all the posts 

Scenario Outline: Verify user is able to get the posts by id
	When User requests for media post by Id = <Id>
	Then Verify user gets requested post
Examples:
	| Id |
	| 1  |
	| 23 |
	| 50 |
	| 87 |

@create
Scenario: Verify user is able to Create New Post
	When User creates new post
	Then Verify new post has been created succesfully.

	
@update
Scenario: Update existing media Post
	When User updates media post for Id = 4
	Then Verify media post updated succesfully for id = 4

@patch
Scenario: Update title of existing Post
	When User updates title of post for Id = 4
	Then Verify media post title updated succesfully for id = 4

@delete
Scenario: Verify user can Delete post
	When User deletes media post for id = 5
	Then Verify media post is deleted succesfully

	
@Comment
Scenario Outline: Verify user can get comments for given id
	When User requests all the comments for post id <Id>
	Then Verify the response returns requested comments
Examples:
	| Id |
	| 1  |
	| 23 |
	| 50 |
	| 87 |


@Comment
Scenario Outline: Verify user can get comments for given id by query parameter
	When User requests all the comments by query parameter for post id <Id> 
	Then Verify the response returns requested comments
Examples:
	| Id |
	| 1  |
	| 2 |
	| 3 |




