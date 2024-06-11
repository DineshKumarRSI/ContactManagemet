# Performance Considerations

## Fast
Our system utilizes a file database in JSON format, ensuring it remains lightweight and fast. This configuration allows for the efficient handling of records ranging from 1 to over 10,000. Additionally, the database includes validation for contact fields, eliminating the possibility of storing invalid data. We can retrieve both single and multiple records in a single call, further enhancing performance.

## Scalability
Our application is built on an N-layer architecture. This design ensures that if we need to transition from a file database to a different type of database, only the Infrastructure layer needs modification. The rest of the layers remain unaffected, ensuring seamless scalability and maintainability.

## Setup Instructions
### Required
- Backend: .NET Core 7
- Frontend: Angular 15.2.10
- Angular CLI: 15.2.11
- Node: 16.14.0


### Steps
* Open Backend Project:
  * Open the ContactApplication solution in Visual Studio.
  * Run the ContactApplication.API project.
* Open Frontend Project:
  * Navigate to the contact-management-app directory in Visual Studio.
  * Execute the command npm install to install all necessary packages.
* Start the Application:
  * Once all packages are installed, run the command npm start.
* Access the Application:
  * The application will automatically open in your default browser.
