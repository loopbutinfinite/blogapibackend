# Blog Backend API - Project Overview

## Project Goal

Create a backend for blog application:
- Support full CRUD functions
- All users to create account and login
- Uses SCRUM workflow concepts
- Introduces Azure DevOps practices

## Stack (What is being used to build this out)
- Back end wil be in .Net 9, ASP.Net Core, EF Core, SQL Server
- Front end will be done in Next.JS with TypeScript, FlowBite(TailWind). Deployed using either Vercel or Azure. 

## Application Features

### User Capabilities

- Create an account
- Login
- Delete account

### Blog Features
- View all published blog posts
- Filter blog posts
- Create new posts
- Edit existing posts
- Delete blog post
- Publish and unpublish posts

### Pages (Frontend connected to our API)
- Create account page
- Blog view posts page of published items
- Dashboard page (This is the profile page which wil edit, delete, and publish/unpublish our blog posts)
- **Blog Page**
    - Display all published blog items
- **Dashbaord Page**
    - User profile page
    - Create blog posts
    - Edit blog posts
    - Delete blog posts

## Project Folder Structure

### Controllers

#### UserController

- Will handle all user interactions
- **Endpoints**
    - Login
    - Add user
    - Update user
    - Delete user

#### BlogController
- Will handle all blog operations
- **Endpoints**
    - Create Blog Item (C)
    - Get all blog items (R)
    - Get blog items by category (R)
    - Get blog items by tags (R)
    - Get blog items by date (R)
    - Get published blog items (R)
    - Get blog items by UserID (R)
    - Update blog items (U)
    - Delete blog items (D)

## Models

### UserModel

```csharp

int Id
string Username
string Salt
string Hash //Consists of 256 characters

```
### BlogModel

```csharp
 
int Id
int UserId
string PublisherName
string Title
string Image
string Description
string Date
string Category
string Tags
bool isPublished
bool isDeleted

```

## Items saved to our Database

### We need a way to sign in with our username and password


### LoginModel

```csharp

string Username
string Password

```

### CreateAccountModel

```csharp

int Id
string Username
string Password

```
### Password Model

```csharp

string Salt
string Hash

```

### Services
Context/Folder
    - DataContext
    - UserService/file
        -GetUserByUsername
        -Login
        -AddUser
        -UpdatedUser
        -DeleteUser

### BlogItemService
- AddBlogItems
- GetAllBlogItems
- GetBlogItemsByCategory
- GetBlogItemsByTags
- GetBlogItemsByDate
- GetPublishedBlogItems
- UpdateBlogItems
- DeleteBlogItems
- GetUserById

### Password Service
- Hash Password
- Very Hash Password