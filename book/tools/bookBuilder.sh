#! /usr/bin/env bash
./book/vendor/caseEsacCompiler compile bookBuilder book/tools/bookBuilder/build/bookBuilder.sh book/tools/bookBuilder/src || exit $?
chmod +x book/tools/bookBuilder/build/bookBuilder.sh
./book/tools/bookBuilder/build/bookBuilder.sh "$@"