local previousText="$1"
local previousUrl="$2"

local previousTemplate="$( < "$WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE" )"
previousTemplate="${previousTemplate//PREVIOUS-TEXT/$previousText}"
previousTemplate="${previousTemplate//PREVIOUS-URL/$previousUrl}"

pageContent+="${NEWLINE}$previousTemplate${NEWLINE}"