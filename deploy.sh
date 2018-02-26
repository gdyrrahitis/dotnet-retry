ApiKey=$1
Source=$2

nuget pack ./DotNetRetry/DotNetRetry.nuspec -Verbosity detailed
nuget push ./DotNetRetry/DotNetRetry.*.nupkg -Verbosity detailed -ApiKey=$1 -Source=$2