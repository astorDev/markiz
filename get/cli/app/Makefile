run:
	dotnet run -- get . --name John

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Get.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Get.Cli

uninstall:
	dotnet tool uninstall --global Get.Cli

reinstall:
	make uninstall || true
	make install