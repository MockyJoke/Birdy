rmdir /S /Q .local\Staging\Birdy
md .local\Staging\Birdy\
md .local\Staging\Birdy\ClientApp\dist\
xcopy ..\..\..\Birdy\bin\Debug\netcoreapp2.1 .local\Staging\Birdy\
xcopy ..\..\..\Birdy\ClientApp\dist\ClientApp .local\Staging\Birdy\ClientApp\dist\
start .local\Staging\Birdy
