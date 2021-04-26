if [[ "$1" = *'%'* ]]
then
  local format="$1"
  shift
  FN log bold red "%s $format" "[Error]" "$@" >&2
else
  FN log bold red "[Error]" "$@" >&2
fi
exit 1