# Objector

Frontend repository can be found here:
https://github.com/andrzejtenus/projektDomanski/tree/master

## About
Application is able to detect objects in pictures.

User is able to upload his foto to the application and after clasification results are displayed:

![image](https://user-images.githubusercontent.com/32637113/153249137-f53527f2-4fe5-4449-98d6-35b380f6e0f4.png)

User is also asked for feedback: 

![image](https://user-images.githubusercontent.com/32637113/153249220-6daff6a3-2f2e-4367-bdec-a3421a9fded7.png)

All answered questions are stored in mongoDB with possibility to aggregate them and display later:  

![image](https://user-images.githubusercontent.com/32637113/153249317-d05a7fa9-9b01-4f17-bd93-d5d4d16385dd.png)


## Getting started
### Requirements

**.NET Core 6.0 SDK**  
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.102-windows-x64-installer

**MongoDB**  
https://docs.mongodb.com/manual/installation/

### Starting aplication

#### Using Visual Studio (requires Visual Studio 2022)
Run Objector.sln file with visual studio. 

Objector web project should be set as default startup project:

![image](https://user-images.githubusercontent.com/32637113/153242645-f0023caa-1649-47c3-9707-af629b81d58e.png)

Click big green arrow at the top of the window:

![image](https://user-images.githubusercontent.com/32637113/153242277-0a9a15cc-99a0-4d15-bbc4-50af51013223.png)

#### Using command line interface
- Move to Objector project folder:
```
cd Objector
```
To start dotnet web service:
```
dotnet run
```
or if you want it to auto recompile after changes:
```
dotnet watch run
```
### Accesing API

After succesfull startup (no errors in command line or visual studio window) site should be accesible under following url: https://localhost:7155/swagger

![image](https://user-images.githubusercontent.com/32637113/153244429-a7618d2c-7a38-4e6a-a797-70d7c211e473.png)
