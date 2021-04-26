#! /usr/bin/env bash
./vendor/caseEsacCompiler compile bookBuilder tools/bookBuilder/build/bookBuilder.sh tools/bookBuilder/src || exit $?
chmod +x tools/bookBuilder/build/bookBuilder.sh
tools/bookBuilder/build/bookBuilder.sh "$@"