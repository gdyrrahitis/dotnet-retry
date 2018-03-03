ApiKey=$1
Source=$2
Version=$3

nuget pack DotNetRetry.csproj -MsbuildPath /usr/lib/mono/msbuild/15.0/bin/ -Prop Configuration=Release -Version $Version -Verbosity detailed -OutputDirectory .
nuget push gdyrra.dotnet.retry.$Version.nupkg -ApiKey $ApiKey -Source $Source