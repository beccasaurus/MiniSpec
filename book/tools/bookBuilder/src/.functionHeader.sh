local -r BOOK="How-to Write a Testing Framework"
local -r AUTHOR="Rebecca Taylor"

local -r NEWLINE=$'\n'
local -r HR="${NEWLINE}${NEWLINE}---${NEWLINE}${NEWLINE}"

local -r SOURCE_DIR=book/chapters
local -r TEMPLATE_DIR=book/templates
local -r WEBSITE_DIR=docs

local -r FULL_SOURCE_DIR=book/source
local -r FULL_SOURCE_FILE="$FULL_SOURCE_DIR/FullSource.md"
local -r FULL_SOURCE_METADATA_FILE="$FULL_SOURCE_DIR/metadata.sh"

local -r WEBSITE_NAVIGATION_FILE="$WEBSITE_DIR/_data/navigation.yml"
local -r WEBSITE_PAGES_DIR="$WEBSITE_DIR/_pages"
local -r WEBSITE_BOOK_CONTENT_DIR="$WEBSITE_PAGES_DIR/book"
local -r WEB_TEMPLATES_DIR="$TEMPLATE_DIR/web"
local -r WEB_THREEDOTS_TEMPLATE_FILE="$WEB_TEMPLATES_DIR/threedots.html"
local -r WEB_PAGE_TEMPLATES_DIR="$WEB_TEMPLATES_DIR/pages"
local -r WEB_PAGE_FRONTMATTER_TEMPLATE_FILE="$WEB_PAGE_TEMPLATES_DIR/frontMatter.md"
local -r WEB_PAGE_NEXT_PREVIOUS_LINKS_TEMPLATE_FILE="$WEB_PAGE_TEMPLATES_DIR/nextPreviousLinks.md"
local -r WEB_PAGE_NEXT_LINK_TEMPLATE_FILE="$WEB_PAGE_TEMPLATES_DIR/nextLink.md"
local -r WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE="$WEB_PAGE_TEMPLATES_DIR/previousLink.md"
local -r WEB_NAVIGATION_TEMPLATES_DIR="$WEB_TEMPLATES_DIR/navigation"
local -r WEB_NAVIGATION_ROOT_TEMPLATE_FILE="$WEB_NAVIGATION_TEMPLATES_DIR/topWebsiteNavigation.yml"
local -r WEB_NAVIGATION_SIDEBAR_TEMPLATE_FILE="$WEB_NAVIGATION_TEMPLATES_DIR/sidebar.yml"
local -r WEB_NAVIGATION_TITLE_TEMPLATE_FILE="$WEB_NAVIGATION_TEMPLATES_DIR/title.yml"
local -r WEB_NAVIGATION_ITEM_TEMPLATE_FILE="$WEB_NAVIGATION_TEMPLATES_DIR/item.yml"

local -r PDF_OUTPUT_FILE="$WEBSITE_DIR/HowToAuthorATestingFramework_byRebeccaTaylor.pdf"
local -r PDF_FULL_SOURCE_FILE="$FULL_SOURCE_DIR/FullSource_PDF.md"
local -r PDF_TEMPLATES_DIR="$TEMPLATE_DIR/pdf"
local -r PDF_LATEX_TEMPLATE_NAME="eisvogel"
local -r PDF_LATEX_COVER_IMAGE_TEMPLATE_FILE="$PDF_TEMPLATES_DIR/$PDF_LATEX_TEMPLATE_NAME-cover-image.latex"
local -r PDF_LATEX_SIMPLE_TEMPLATE_FILE="$PDF_TEMPLATES_DIR/$PDF_LATEX_TEMPLATE_NAME-cover-text.latex"
local -r PDF_LATEX_PREAMBLE_FILE="$PDF_TEMPLATES_DIR/preamble.latex"
local -r PDF_PANDOC_TEMPLATE_DIRECTORY="$HOME/.pandoc/templates"
local -r PDF_FULL_SOURCE_HEADER_FILE="$PDF_TEMPLATES_DIR/FullSourceHeader.md"
local -r PDF_FULL_SOURCE_HEADER_NO_FRONTBACK_FILE="$PDF_TEMPLATES_DIR/FullSourceHeaderNoFrontBack.md"

local -r EPUB_OUTPUT_FILE="$WEBSITE_DIR/HowToAuthorATestingFramework_byRebeccaTaylor.epub"
local -r EPUB_FULL_SOURCE_FILE="$FULL_SOURCE_DIR/FullSource_ePub.md"
local -r EPUB_TEMPLATES_DIR="$TEMPLATE_DIR/epub"
local -r EPUB_FULL_SOURCE_HEADER_FILE="$EPUB_TEMPLATES_DIR/header.md"
local -r EPUB_CSS_FILE="$EPUB_TEMPLATES_DIR/epub.css"

# Common Locals (cannot be changed by child functions but are inherited)
local -i part chapter section subSection smallSection minorSection
declare -a chapterIds sectionIds subSectionIds smallSectionIds minorSectionIds
local partTitle chapterTitle sectionTitle subSectionTitle smallSectionTitle minorSectionTitle \
      partSafeTitle chapterSafeTitle sectionSafeTitle subSectionSafeTitle smallSectionSafeTitle minorSectionSafeTitle