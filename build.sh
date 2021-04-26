#! /usr/bin/env bash

export VERBOSE=true

./tools/bookBuilder.sh source recompile

case "$1" in
  web)  ./tools/bookBuilder.sh web generate ;;
  pdf)  ./tools/bookBuilder.sh pdf generate ;;
  epub) ./tools/bookBuilder.sh epub generate ;;
  *)    ./build.sh web && ./build.sh pdf ;;
esac