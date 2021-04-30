local page=''
local part=$1
local chapter="$2"
local -a chapters
read -ra chapters <<< "${PART_CHAPTERS[$part]}"
local -a sections
read -ra sections <<< "${CHAPTER_SECTIONS[$chapter]}"
local startLine="${CHAPTER_START_LINES[$chapter]}"
local endLine="${CHAPTER_END_LINES[$chapter]}"
(( ${#sections[@]} > 0 )) && (( endLine = ${SECTION_START_LINES[${sections[0]}]} - 1 ))

local navigation="${CHAPTER_URLS[$chapter]//[^a-zA-Z0-9_]/}"

FN web navigation newSidebar "$navigation"

local bookPart
for bookPart in "${!PART_TITLES[@]}"
do
  if (( bookPart == part ))
  then
    FN web navigation addTitle "<strong>${PART_TITLES[$part]}</strong>" "${PART_URLS[$part]}"
  else
    FN web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
  fi

  local foundChapter=
  local nextChapter=
  local partChapter
  for partChapter in ${PART_CHAPTERS[$bookPart]}
  do
    if (( partChapter == chapter ))
    then
      foundChapter=true
      FN web navigation addTitle "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
      local chapterSection
      for chapterSection in "${sections[@]}"
      do
        FN web navigation addItem "» ${SECTION_TITLES[$chapterSection]}" "${SECTION_URLS[$chapterSection]}"
      done
    else
      [ "$foundChapter" = true ] && [ -z "$nextChapter" ] && nextChapter="$partChapter"
      (( bookPart == part )) && FN web navigation addTitle "<i>• ${CHAPTER_TITLES[$partChapter]}</i>" "${CHAPTER_URLS[$partChapter]}"
    fi
  done
done

page+="$( PARENT frontMatter "${CHAPTER_TITLES[$chapter]}" "${CHAPTER_URLS[$chapter]}" "$navigation" )${NEWLINE}"

page+="<h1><a href=\"${PART_URLS[part]}\">${PART_TITLES[part]}</a></h1>${NEWLINE}${NEWLINE}"
page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )"
page+="${HR}"

if (( ${#sections[@]} > 0 ))
then
  page+="$( PARENT nextLink "${SECTION_TITLES[${sections[0]}]}" "${SECTION_URLS[${sections[0]}]}" )"
elif [ -n "$nextChapter" ]
then
  page+="$( PARENT nextLink "${CHAPTER_TITLES[$nextChapter]}" "${CHAPTER_URLS[$nextChapter]}" )"
elif (( part < ${#PART_TITLES[@]} - 1 ))
then
  page+="$( PARENT nextLink "${PART_TITLES[$(( part + 1 ))]}" "${PART_URLS[$(( part + 1 ))]}" )"
fi

if (( chapter == ${chapters[0]} )) && (( part > 0 ))
then
  page+="$( PARENT previousLink "${PART_TITLES[part]}" "${PART_URLS[part]}" )"
else
  page+="$( PARENT previousLink "${CHAPTER_TITLES[$(( chapter - 1 ))]}" "${CHAPTER_URLS[$(( chapter - 1 ))]}" )"
fi

mkdir -p "${CHAPTER_FILE_PATHS[$chapter]%/*}"

printf '%s' "$page" > "${CHAPTER_FILE_PATHS[$chapter]}"
