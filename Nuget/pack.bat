@Echo Setting up folder structure
md Package\lib\net45\
md Package\tools\

md Package\content\lang\

md Package\content\
md Package\content\modules\
md Package\content\modules\_protected\
md Package\content\modules\_protected\VisitorGroupUsage\
md Package\content\modules\_protected\VisitorGroupUsage\Scripts\
md Package\content\modules\_protected\VisitorGroupUsage\Views\

REM md Package\content\ClientResources\Scripts\visitorgroupusage\
REM md Package\content\Views\VisitorGroupUsage\

md Package\content\modules\_protected\VisitorGroupUsage\Scripts\
md Package\content\modules\_protected\VisitorGroupUsage\Views\

@Echo Removing old files
del /Q Package\lib\net45\*.*
del /Q Package\tools\*.*
del /Q Package\content\*.*

@Echo Copying new files
REM copy ..\VisitorGroupUsage\module.config.transform Package\content
REM copy ..\VisitorGroupUsage\bin\Release\VisitorGroupUsage.dll Package\lib\net45
REM copy ..\VisitorGroupUsage\ClientResources\Scripts\visitorgroupusage\viewer.js Package\content\ClientResources\Scripts\visitorgroupusage\viewer.js
REM copy ..\VisitorGroupUsage\Views\VisitorGroupUsage\Index.cshtml Package\content\Views\VisitorGroupUsage\Index.cshtml
REM copy ..\VisitorGroupUsage\Views\VisitorGroupUsage\VisitorGroupListing.cshtml Package\content\Views\VisitorGroupUsage\VisitorGroupListing.cshtml
REM copy ..\VisitorGroupUsage\lang\VisitorGroupUsageViewer.xml Package\content\lang\VisitorGroupUsageViewer.xml

copy ..\VisitorGroupUsage\bin\Release\VisitorGroupUsage.dll Package\lib\net45
copy ..\VisitorGroupUsage\modules\_protected\VisitorGroupUsage\module.config Package\content\modules\_protected\VisitorGroupUsage\
copy ..\VisitorGroupUsage\modules\_protected\VisitorGroupUsage\Views\web.config Package\content\modules\_protected\VisitorGroupUsage\Views\
copy ..\VisitorGroupUsage\modules\_protected\VisitorGroupUsage\Scripts\viewer.js Package\content\modules\_protected\VisitorGroupUsage\Scripts\viewer.js
copy ..\VisitorGroupUsage\modules\_protected\VisitorGroupUsage\Views\Index.cshtml Package\content\modules\_protected\VisitorGroupUsage\Views\Index.cshtml
copy ..\VisitorGroupUsage\modules\_protected\VisitorGroupUsage\Views\VisitorGroupListing.cshtml Package\content\modules\_protected\VisitorGroupUsage\Views\VisitorGroupListing.cshtml
copy ..\VisitorGroupUsage\lang\VisitorGroupUsageViewer.xml Package\content\lang\VisitorGroupUsageViewer.xml

@Echo Packing files
"..\.nuget\nuget.exe" pack package\VisitorGroupUsage.nuspec

@Echo Moving package
move /Y *.nupkg c:\project\nuget.local\