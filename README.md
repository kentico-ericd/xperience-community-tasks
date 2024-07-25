# Xperience by Kentico Tasks

[![Nuget](https://img.shields.io/nuget/v/Xperience.Labs.Tasks)](https://www.nuget.org/packages/Xperience.Labs.Tasks#versions-body-tab)
[![build](https://github.com/kentico-ericd/xperience-by-kentico-tasks/actions/workflows/build.yml/badge.svg)](https://github.com/kentico-ericd/xperience-by-kentico-tasks/actions/workflows/build.yml)

## Description

This is a basic implementation of [Scheduled tasks](https://docs.kentico.com/13/configuring-xperience/scheduling-tasks) for Xperience by Kentico. This package does _not_ store information in the database or contain UI pages. This means that tasks are executed when the application starts, then execute based on their provided intervals.

## Library Version Matrix

| Xperience Version | Library Version |
| ----------------- | --------------- |
| 29.x.y            | 1.x.y           |

## Package Installation

Add the package to your application using the .NET CLI

```powershell
dotnet add package Xperience.Labs.Tasks
```

## Quick Start

Add the following to your application's startup code:

```cs
builder.Services.AddKenticoTasks();
...
app.StartKenticoTasks();
```

Create one or more classes implementing `IXperienceTask` to run your custom code:

```cs
public class MyTask : IXperienceTask
{
    public XperienceTaskSettings Settings => new(nameof(MyTask), 1);

    public void Execute()
    {
        // Do something...
    }
}
```

## Full Instructions

View the [Usage Guide](./docs/Usage-Guide.md) for more detailed instructions.
