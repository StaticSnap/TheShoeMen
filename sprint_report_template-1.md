# Sprint x Report 
Video Link: https://cole.ws/+451project
## What's New (User Facing)
 * Login
 * Signup
 * Web server (web site hosted for users)

## Work Summary (Developer Facing)
This sprint, we got the fundamental structure of our web application set up including a blazor web server, SQLite database, login/registration implementations, and basic role-based access with cookies. 

## Unfinished Work
The user-side cookies for persistent login are currently being worked on, alongside the functionality of viewing the catalog of shoes. We have some work done within the main shop of the site, but it is the largest component of the site and will require another sprint to complete.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/StaticSnap/TheShoeMen/issues/2
 * https://github.com/StaticSnap/TheShoeMen/issues/4
 * https://github.com/StaticSnap/TheShoeMen/issues/1

 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * https://github.com/StaticSnap/TheShoeMen/issues/5
 - We had time to implement a working login system but not a secure one, within the next sprint we aim to have password hashing with BCrypt and protected routes so that you cannot send a post request with any SQL statement inside directly to our database.
 * https://github.com/StaticSnap/TheShoeMen/issues/3
 - We need to have a lot of shoe types and each with colors, this is time consuming and we only had time to implement some example shoes for testing.
 * https://github.com/StaticSnap/TheShoeMen/issues/6
 - This is the main functionality of the site and also the largest aspect. We did not plan to fully complete this because it is constantly changing as the project evolves.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [Login.razor](https://github.com/StaticSnap/TheShoeMen/blob/master/GroupProject/Components/Pages/Login.razor)
 * [Createaccount.razor](https://github.com/StaticSnap/TheShoeMen/blob/master/GroupProject/Components/Pages/Createaccount.razor)
 * [Home.razor](https://github.com/StaticSnap/TheShoeMen/blob/master/GroupProject/Components/Pages/Home.razor)
 
## Retrospective Summary
Here's what went well:
  * Learning c# and its use within Razor as a group.
  * Completing critical issues on time so other team members could build ontop of the functionality.
 
Here's what we'd like to improve:
   * We would like to complete more issues within the next spring.
   * We need to improve the overall security of the applciation.
  
Here are changes we plan to implement in the next sprint:
   * We plan to manage time better within the alloted sprint, allowing us to only need to polish the app within the last sprint.
   * More detailed issues within the Kanban board.