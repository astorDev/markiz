run:
	dotnet run -- get . --name John

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Markiz.Get.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Markiz.Get.Cli

uninstall:
	dotnet tool uninstall --global Markiz.Get.Cli

reinstall:
	make uninstall || true
	make install