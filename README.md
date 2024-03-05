# Web-Application-Development

# Web Application Project 

The aim of this project is to create a .NET Core (C#) RESTful backend service with the following features: 
- A database with at least 5 tables, SQLite
- Use of JWT token for user authentication
- Integration of IF for suer mamanegemnt, roles and access control
- Email service used when the user has signed up

The specific project cretaed here is basing it off a company that would run projects. The 5 database tables are: Employee, Projects, Expense, Client and Department.
This is a university project and only creates the backend service.

## Getting Started

All the files on GitHub are the necessary files to run the project however the JSON files are not provided so there will be errors if tried to run without them. 

### Usage

This code can be consumed using Swagger or Postman. Since this is just the backend there is no frontend presentation. However using Swagger or Postman it is possible to create modify and delete instances from the 5 tables listed above: Employee, Clients, Department, Projects and Expense.

Some of the tables are only accessible to admin roles such as the employee and the roles table. The role methods are used to assign roles to a user after it has signed up. This program was written so that the first ever account to sign up will be automatically assigned the admin role. This admin role gives the privileges to accessing the table mentioned above and so to assign roles to users. After sigming up, when the user login the outpu will give a token to the user. This token needs to be added to the authorize pages respectively of Postman or Swagger to give the user its privileges. 

It is also possible to delete an account just by entereing the email adress. 

#### Contribution

As mentionned before this is a univeristy project so contribution on this project is not expected.

##### Contact

Even if contribution is not wanted to this project, if there is a need to contact me about this project it is possible at the following email adress: yoav.nathan@outlook.com

###### Versioning

This project has been version controlled using branches for new features that were added. After a feature was added and working properly the feature branched was merged to the developed branch. So and so on until the project was completed and the main branch was then modified.
