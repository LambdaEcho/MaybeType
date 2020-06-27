#Requires -Version 5.0

$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$versionXmlAbsoluteFilePath = Join-Path $here "..\build\version.props"

Write-Output "Reading $($versionXmlAbsoluteFilePath)..."
[xml]$xml = Get-Content -Path $versionXmlAbsoluteFilePath
$targetVersion = (Select-Xml -Xml $xml -XPath "//PropertyGroup/XTargetSemVer").Node.InnerText
Write-Output "Got XTargetSemVer '$($targetVersion)'"

$solutionAbsoluteFilePath = Join-Path $here "..\src"
dotnet pack $solutionAbsoluteFilePath -c Release --include-symbols /p:InformationalVersion=$targetVersion
