(( $# == 0 )) && FN error "PARENT addTitle requires 1-2 arguments: [item] [url (optional)]"

local text="$1"
local safeText=''

# Replace ` with <code> blocks
local codeBlockOpen=
local -i i
for (( i = 0; i < ${#text}; i++ ))
do
  if [ "${text:$i:1}" = '`' ]
  then
    if [ -z "$codeBlockOpen" ]
    then
      codeBlockOpen=true
      safeText+="<code>"
    else
      codeBlockOpen=
      safeText+="</code>"
    fi
  else
    safeText+="${text:$i:1}"
  fi
done

local itemTemplate="$( < "$WEB_NAVIGATION_ITEM_TEMPLATE_FILE" )"
itemTemplate="${itemTemplate/ITEM-TEXT/$safeText}"
itemTemplate="${itemTemplate/ITEM-URL/${2:-\#}}"

printf '%s' "$itemTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
