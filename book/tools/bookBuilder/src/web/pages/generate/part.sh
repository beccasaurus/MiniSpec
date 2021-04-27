local page=''
local part=$1
local -a chapters
read -ra chapters <<< "${PART_CHAPTERS[$part]}"
local startLine="${PART_START_LINES[$part]}"
local endLine="${PART_END_LINES[$part]}"
(( ${#chapters[@]} > 0 )) && (( endLine = ${CHAPTER_START_LINES[${chapters[0]}]} - 1 ))

local navigation="${PART_URLS[$part]//[^a-zA-Z0-9_]/}"

FN web navigation newSidebar "$navigation"

local bookPart
for bookPart in "${!PART_TITLES[@]}"
do
  if (( bookPart == part ))
  then
    FN web navigation addTitle "${PART_TITLES[$bookPart]}" "${PART_URLS[$bookPart]}"
    local partChapter
    for partChapter in ${PART_CHAPTERS[$bookPart]}
    do
      FN web navigation addItem "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
    done
  else
    FN web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
  fi
done

page+="$( PARENT frontMatter "${PART_TITLES[$part]}" "${PART_URLS[$part]}" "$navigation" )${NEWLINE}"
page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )"
page+="${HR}"
if (( ${#chapters[@]} > 0 ))
then
  (( part == ${#PART_TITLES[@]} - 1 )) || page+="$( PARENT nextLink "${CHAPTER_TITLES[${chapters[0]}]}" "${CHAPTER_URLS[${chapters[0]}]}" )"
else
  (( part == ${#PART_TITLES[@]} - 1 )) || page+="$( PARENT nextLink "${PART_TITLES[$(( part + 1 ))]}" "${PART_URLS[$(( part + 1 ))]}" )"
fi
(( part != 0 )) && page+="$( PARENT previousLink "${PART_TITLES[$(( part - 1 ))]}" "${PART_URLS[$(( part - 1 ))]}" )"

mkdir -p "${PART_FILE_PATHS[$part]%/*}"

printf '%s' "$page" > "${PART_FILE_PATHS[$part]}"