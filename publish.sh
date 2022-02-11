#!/bin/bash

dotnet publish --self-contained --os $1 /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true
