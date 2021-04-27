#! /usr/bin/env bash

export VERBOSE=true

./book/tools/bookBuilder.sh source recompile

case "$1" in
  web)  shift; ./book/tools/bookBuilder.sh web generate  "$@" ;;
  pdf)  shift; ./book/tools/bookBuilder.sh pdf generate  "$@" ;;
  epub) shift; ./book/tools/bookBuilder.sh epub generate "$@" ;;
  *)    ./makeBook.sh web && ./makeBook.sh pdf ;;
esac