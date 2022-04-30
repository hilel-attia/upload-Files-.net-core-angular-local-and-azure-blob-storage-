# Stargate File Upload Service
### A VueJS interface and .NET Core API for uploading files to storage
[![build status](https://ares3.visualstudio.com/_apis/public/build/definitions/5b5391a8-01e1-46ad-8c3d-57f3ba63f0ea/2/badge)](https://ares3.visualstudio.com/stargate/_build/index?definitionId=2)
[![stargate demo](https://img.shields.io/badge/stargate-demo-blue.svg)](https://stargate.thesharpdev.com)

Currently supported File Storage Backend's

* Azure File Storage
* Azure Blob Storage

![Stargate Screenshot](https://i.imgur.com/tLfHqxk.png)

### Installation

1. Clone the repo
2. Build
  * **API**
     * Build the solution using Visual Studio 2017
  * **Web**
     * `npm install`
     * `npm run dev`

### Configuration

**Web**

Set the `API_HOST` and `API_PORT` values in the `config/dev.env.js`

**API**

If you're using Azure storage you will need to create an environment variable `stargate:azureStorageAccount` with your Connection String.

```
TIP: You can find your connection string from the "Access keys" menu option for your storage account
```

**_Troubleshooting_**

You may need to restart the npm http server or Visual Studio to pick up environment variable/configuration changes.

### Roadmap

* Unit Tests
* Hosted Example
* Host multiple files with the same name
* Built in URL Shortner
* Plugin Support
