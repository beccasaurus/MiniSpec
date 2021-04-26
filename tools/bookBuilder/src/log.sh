[ "$VERBOSE" = true ] || return
 
local usePrintf=
local printfFormat=
local colorName=
local colorNumber=
local fontBold=
local fontLight=
local fontWeight=

while true
do
  case "$1" in
    red | green | yellow | blue | magenta | cyan | gray | grey) colorName="$1"; shift ;;
    bold) fontBold=true; shift; ;;
    light) fontLight=true; shift; ;;
    *'%'*) usePrintf=true; printfFormat="$1"; shift ;;
    *) break ;;
  esac
done

if [ -n "$colorName" ]
then
  case "$colorName" in
    red) colorNumber=31 ;;
    green) colorNumber=32 ;;
    yellow) colorNumber=33 ;;
    blue) colorNumber=34 ;;
    magenta) colorNumber=35 ;;
    cyan) colorNumber=36 ;;
    gray | grey) colorNumber=37 ;;
  esac
fi

if [ -n "$fontBold" ]
then
  fontWeight=";1"
  [ -z "$colorNumber" ] && colorNumber=39
fi

[ -n "$fontLight" ] && let colorNumber="colorNumber + 60"

if [ -n "$colorNumber" ]
then
  if [ -n "$usePrintf" ]
  then
    printf "\e[$colorNumber${fontWeight}m$printfFormat\e[0m" "$@"
  else
    echo -e "\e[$colorNumber${fontWeight}m$*\e[0m"
  fi
else
  if [ -n "$usePrintf" ]
  then
    printf "$printfFormat" "$@"
  else
    echo "$@"
  fi
fi