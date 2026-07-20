PROJECT     := AssetRouter.csproj
CONFIG      := Release
PUBLISH_DIR := bin/Release/net10.0/publish/wwwroot
PORT        := 8080

.PHONY: help dev build format clean tools publish serve

help:
	@echo "Perintah:"
	@echo "  make dev      - dev server + hot reload (dotnet watch)"
	@echo "  make build    - build Debug"
	@echo "  make format   - rapikan kode sesuai .editorconfig"
	@echo "  make publish  - build production (Release: trimmed + compressed)"
	@echo "  make serve    - publish lalu serve di http://localhost:$(PORT)"
	@echo "  make tools    - install dotnet-serve (sekali saja)"
	@echo "  make clean    - hapus artefak build"

dev:
	dotnet watch

build:
	dotnet build

format:
	dotnet format

publish:
	dotnet publish -c $(CONFIG)

serve: publish
	dotnet serve -d $(PUBLISH_DIR) -p $(PORT)

tools:
	dotnet tool update --global dotnet-serve

clean:
	dotnet clean
