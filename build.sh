#! /usr/bin/env bash

dotnet nuget locals --clear all
# git clean -fdx
find . -type d -name obj -exec rm -rfv {} \;
find . -type d -name bin -exec rm -rfv {} \;
cd MiniSpec
dotnet build
(( $? == 0 )) || exit $?

[ "$1" = --no-test ] && exit 0
cd ../spec
dotnet test "$@"