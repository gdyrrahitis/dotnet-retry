ApiKey=$1
Source=$2
Version=$3

nuget pack -Prop Configuration=Release -Version $Version -Verbosity detailed -OutputDirectory .
nuget push gdyrra.dotnet.retry.$Version.nupkg -ApiKey $ApiKey -Source $Source