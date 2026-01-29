# EncryptionApi

A simple ASP.NET Core Web API for encryption services, with static file hosting and CORS enabled.

## Features

- REST API endpoints for encryption (see `Api/Controllers/EncryptionController.cs`)
- Serves static files (e.g., `Index.html`)
- CORS enabled for all origins, methods, and headers
- Deployed to AWS Elastic Beanstalk on URL http://decryptionapp-env.eba-8nqpy8tw.us-east-1.elasticbeanstalk.com/Index.html

## Project Structure

```
.
├── Api/
│   ├── Controllers/
│   │   └── EncryptionController.cs
│   ├── Index.html
│   ├── Program.cs
│   ├── Api.csproj
│   └── Procfile
├── Tests/
│   └── EncryptionControllerTests.cs
├── .platform/
│   └── hooks/
│       └── postdeploy/
│           └── 01_configure_nginx.sh
├── .ebextensions/
│   └── nginx-proxy.config
└── .github/
    └── workflows/
        └── aws.yml
```

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- (Optional) AWS CLI & Elastic Beanstalk CLI for deployment

### Build and Run Locally

```bash
cd Api
dotnet restore
dotnet build
dotnet run
```

Visit [http://localhost:5000/Index.html](http://localhost:5000/Index.html) in your browser.

### Run Tests

```bash
cd Tests
dotnet test
```

### Publish

```bash
cd Api
dotnet publish -c Release
```

### Deploy to AWS Elastic Beanstalk

1. Ensure `.platform` and `.ebextensions` are in your deployment bundle.
2. Deploy using the AWS EB CLI or your CI/CD pipeline.

## API Endpoints

See `Api/Controllers/EncryptionController.cs` for available endpoints.

## Static Files

- Place static files (e.g., `Index.html`) in the `Api` directory.
- They will be served at the root path (e.g., `/Index.html`).

## Accessing the App on Elastic Beanstalk

After deployment, visit:

```
http://decryptionapp-env.eba-8nqpy8tw.us-east-1.elasticbeanstalk.com/Index.html
```


## Custom Nginx

- The `.platform/hooks/postdeploy/01_configure_nginx.sh` script configures Nginx to proxy requests to Kestrel.

## License

MIT
