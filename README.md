# ⏲️ Xperience Community: Tasks

[![Nuget](https://img.shields.io/nuget/v/Xperience.Community.Tasks)](https://www.nuget.org/packages/Xperience.Community.Tasks#versions-body-tab)
[![build](https://github.com/kentico-ericd/xperience-community-tasks/actions/workflows/build.yml/badge.svg)](https://github.com/kentico-ericd/xperience-community-tasks/actions/workflows/build.yml)

![Task listing](/images/ui.png)

## Description

This is a basic implementation of [Scheduled tasks](https://docs.kentico.com/13/configuring-xperience/scheduling-tasks) for Xperience by Kentico. This package does _not_ store information in the database. This means that tasks do not retain their next execution time or execution count between application restarts. Tasks will run at at application start, after their interval has passed. For example, if the application is started at 7:00, a task with an interval of 5 minutes will execute at 7:05.

## Library Version Matrix

| Xperience Version | Library Version |
| ----------------- | --------------- |
| 29.x.y            | 1.x.y           |

## :gear: Package Installation

Add the package to your application using the .NET CLI

```powershell
dotnet add package Xperience.Community.Tasks
```

## 🚀 Quick Start

Add the following to your application's startup code:

```cs
builder.Services.AddKenticoTasks();
```

Create one or more classes implementing `IXperienceTask` to run your custom code:

```cs
public class MyTask : IXperienceTask
{
    public XperienceTaskSettings Settings => new(nameof(MyTask), 1);

    public Task Execute()
    {
        // Do something...
    }
}
```

## 🗒 Full Instructions

View the [Usage Guide](./docs/Usage-Guide.md) for more detailed instructions.
