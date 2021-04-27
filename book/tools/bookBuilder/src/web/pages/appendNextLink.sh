local nextTemplate="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
nextTemplate="${nextTemplate//NEXT-TEXT/$1}"
nextTemplate="${nextTemplate//NEXT-URL/$2}"

printf '%s' "$nextTemplate"