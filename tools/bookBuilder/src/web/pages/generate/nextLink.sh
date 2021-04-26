local template="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
template="${template//NEXT-TEXT/$1}"
template="${template//NEXT-URL/$2}"
printf '%s' "$template"