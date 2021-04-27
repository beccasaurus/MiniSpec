(( $# == 0 )) && FN error "PARENT newSidebar requires 1 argument: [sidebar name]"

local sidebarTemplate="$( < "$WEB_NAVIGATION_SIDEBAR_TEMPLATE_FILE" )"
sidebarTemplate="${sidebarTemplate//SIDEBAR_NAME/$1}"

printf '%s' "$sidebarTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"