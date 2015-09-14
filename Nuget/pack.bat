@Echo Setting up folder structure
md Package\lib\net45\
md Package\tools\
md Package\content\

md Package\content\ClientResources\Scripts\visitorgroupusage\
md Package\content\Views\VisitorGroupUsage\
md Package\content\lang\


@Echo Removing old files
del /Q Package\lib\net45\*.*
del /Q Package\tools\*.*
del /Q Package\content\*.*

@Echo Copying new files
copy ..\VisitorGroupUsage\module.config.transform Package\content
copy ..\VisitorGroupUsage\bin\Release\VisitorGroupUsage.dll Package\lib\net45
copy ..\VisitorGroupUsage\ClientResources\Scripts\visitorgroupusage\viewer.js Package\content\ClientResources\Scripts\visitorgroupusage\viewer.js
copy ..\VisitorGroupUsage\Views\VisitorGroupUsage\Index.cshtml Package\content\Views\VisitorGroupUsage\Index.cshtml
copy ..\VisitorGroupUsage\Views\VisitorGroupUsage\VisitorGroupListing.cshtml Package\content\Views\VisitorGroupUsage\VisitorGroupListing.cshtml
copy ..\VisitorGroupUsage\lang\VisitorGroupUsageViewer.xml Package\content\lang\VisitorGroupUsageViewer.xml

@Echo Packing files
"..\.nuget\nuget.exe" pack package\VisitorGroupUsage.nuspec

@Echo Moving package
move /Y *.nupkg c:\project\nuget.local\