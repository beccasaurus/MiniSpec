[ -z "$previousText" ] || [ -z "$previousUrl" ] || [ -z "$nextText" ] || [ -z "$nextUrl" ] && FN error "PARENT prependPreviousNextLinks requires configuring \$previousText \$previousUrl \$nextText \$nextUrl"

local prevNextTemplate="$( < "$WEB_PAGE_NEXT_PREVIOUS_LINKS_TEMPLATE_FILE" )"
prevNextTemplate="${prevNextTemplate//NEXT-TEXT/$nextText}"
prevNextTemplate="${prevNextTemplate//NEXT-URL/$nextUrl}"
prevNextTemplate="${prevNextTemplate//PREVIOUS-TEXT/$previousText}"
prevNextTemplate="${prevNextTemplate//PREVIOUS-URL/$previousUrl}"

pageContent="$prevNextTemplate<br />${NEWLINE}${NEWLINE}$pageContent${NEWLINE}"