![Icon](https://i.imgur.com/6pnQJlc.jpg?1)
# JenkinsClient.Net 
[![Build status](https://ci.appveyor.com/api/projects/status/u5whudc4r78cu1ef?svg=true)](https://ci.appveyor.com/project/lvermeulen/jenkinsclient-net)
 [![license](https://img.shields.io/github/license/lvermeulen/JenkinsClient.Net.svg?maxAge=2592000)](https://github.com/lvermeulen/JenkinsClient.Net/blob/master/LICENSE) [![NuGet](https://img.shields.io/nuget/vpre/JenkinsClient.Net.svg?maxAge=2592000)](https://www.nuget.org/packages/JenkinsClient.Net/) 
 ![](https://img.shields.io/badge/.net-4.6-yellowgreen.svg) ![](https://img.shields.io/badge/netstandard-1.6-yellowgreen.svg)

C# client for [Jenkins](https://jenkins.io)

## Features
* [X] Authentication
    * [X] Basic
    * [X] Api token
* [X] System
    * [X] Information
    * [X] Version
    * [X] Security crumb
    * [X] Create job
    * [X] Copy job
    * [X] Quiet down
    * [X] Cancel quiet down
    * [X] Restart
    * [X] Safe restart
* [X] Jobs
    * [X] Fetch/update Configuration
    * [X] Delete
    * [X] Fetch/update description
    * [X] Build
    * [X] Build with parameters
    * [X] Enable/disable
* [X] Builds
    * [X] Build number
    * [X] Build timestamp
    * [X] Build console output
    * [X] Build HTML console output
* [ ] Queue
* [ ] Artifacts
* [ ] Plugin manager
    * [ ] Upload
    * [ ] Prevalidate configuration
    * [ ] Install necessary plugins
