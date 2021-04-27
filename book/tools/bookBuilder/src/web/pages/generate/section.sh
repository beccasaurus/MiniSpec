local page=''
local part=$1
local chapter="$2"
local section="$3"
local -a sections
read -ra sections <<< "${CHAPTER_SECTIONS[$chapter]}"
local startLine="${SECTION_START_LINES[$section]}"
local endLine="${SECTION_END_LINES[$section]}"

local navigation="${SECTION_URLS[$section]//[^a-zA-Z0-9_]/}"

FN web navigation newSidebar "$navigation"

local foundSection=
local nextSection=
local bookPart
for bookPart in "${!PART_TITLES[@]}"
do
  if (( bookPart == part ))
  then
    FN web navigation addTitle "<strong>${PART_TITLES[$part]}</strong>" "${PART_URLS[$part]}"
  else
    FN web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
  fi

  local partChapter
  for partChapter in ${PART_CHAPTERS[$bookPart]}
  do
    if (( partChapter == chapter ))
    then
      FN web navigation addTitle "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
      local chapterSection
      for chapterSection in "${sections[@]}"
      do
        FN web navigation addItem "» ${SECTION_TITLES[$chapterSection]}" "${SECTION_URLS[$chapterSection]}"
        if (( chapterSection == section ))
        then
          foundSection=true
        else
          [ "$foundSection" = true ] && [ -z "$nextSection" ] && nextSection="$chapterSection"
        fi
      done
    else
      (( bookPart == part )) && FN web navigation addTitle "<i>• ${CHAPTER_TITLES[$partChapter]}</i>" "${CHAPTER_URLS[$partChapter]}"
    fi
  done
done

page+="$( PARENT frontMatter "${SECTION_TITLES[$section]}" "${SECTION_URLS[$section]}" "$navigation" "$showMenu" )${NEWLINE}${NEWLINE}"

page+="<h1><a href=\"${PART_URLS[part]}\">${PART_TITLES[part]}</a></h1>${NEWLINE}${NEWLINE}"
page+="<h2><a href=\"${CHAPTER_URLS[chapter]}\">${CHAPTER_TITLES[chapter]}</a></h2>${NEWLINE}${NEWLINE}"

page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )${NEWLINE}"
page+="${HR}"

if [ -n "$nextSection" ]
then
  page+="$( PARENT nextLink "${SECTION_TITLES[$nextSection]}" "${SECTION_URLS[$nextSection]}" )"
elif (( chapter != ${#CHAPTER_SECTIONS[@]} - 1 ))
then
  page+="$( PARENT nextLink "${CHAPTER_TITLES[$(( chapter + 1 ))]}" "${CHAPTER_URLS[$(( chapter + 1 ))]}" )"
else
  : # Last Chapter in the book TODO
fi

if (( section == ${sections[0]} ))
then
  page+="$( PARENT previousLink "${CHAPTER_TITLES[chapter]}" "${CHAPTER_URLS[chapter]}" )"
else
  page+="$( PARENT previousLink "${SECTION_TITLES[$(( section - 1 ))]}" "${SECTION_URLS[$(( section - 1 ))]}" )"
fi

mkdir -p "${SECTION_FILE_PATHS[$section]%/*}"

printf '%s' "$page" > "${SECTION_FILE_PATHS[$section]}"
