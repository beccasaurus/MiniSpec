local template="$( < "$WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE" )"
template="${template//PREVIOUS-TEXT/$1}"
template="${template//PREVIOUS-URL/$2}"
printf '%s' "$template"