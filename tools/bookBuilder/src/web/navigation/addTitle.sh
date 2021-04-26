(( $# == 0 )) && FN error "PARENT addTitle requires 1-2 arguments: [title] [url (optional)]"

local titleTemplate="$( < "$WEB_NAVIGATION_TITLE_TEMPLATE_FILE" )"
titleTemplate="${titleTemplate/TITLE-TEXT/$1}"
titleTemplate="${titleTemplate/TITLE-URL/${2:-\#}}"

printf '%s' "$titleTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
