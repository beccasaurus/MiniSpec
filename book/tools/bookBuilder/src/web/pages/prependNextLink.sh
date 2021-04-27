local nextText="$1"
local nextUrl="$2"

local nextTemplate="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
nextTemplate="${nextTemplate//NEXT-TEXT/$nextText}"
nextTemplate="${nextTemplate//NEXT-URL/$nextUrl}"

pageContent="$nextTemplate<br />${NEWLINE}${NEWLINE}$pageContent"