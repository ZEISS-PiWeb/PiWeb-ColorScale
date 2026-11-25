# PiWeb-ColorScale

| ![Zeiss IQS Logo](docs/logo_128x128.png) | A serializable color scale that returns colors for given numeric values. |
|-|:-|

## Build and publish

The library targets .NET Standard 2.0; its tests run on .NET 10.
NuGet packages are published with GitHub Actions OIDC trusted publishing.
Configure NuGet trusted-publishing policies for `ZEISS-PiWeb/PiWeb-ColorScale`
for `develop.yml`, `create-release.yml`, and `create-support-release.yml`
(workflow filename only; leave the environment unset). The repository also
requires the `NUGET_USER` secret with the NuGet profile name. Release workflows
require `PUSH_TO_GITHUB_REPO_PAT` to push branches and tags.
