#! /usr/bin/env bash

export VERBOSE=true

./tools/bookBuilder.sh source recompile

case "$1" in
  web)  shift; ./tools/bookBuilder.sh web generate  "$@" ;;
  pdf)  shift; ./tools/bookBuilder.sh pdf generate  "$@" ;;
  epub) shift; ./tools/bookBuilder.sh epub generate "$@" ;;
  *)    ./build.sh web && ./build.sh pdf ;;
esac