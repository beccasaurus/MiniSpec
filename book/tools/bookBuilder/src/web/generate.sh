local filterExpression=

while (( $# > 0 ))
do
  case "$1" in
    -c | --compile) FN source recompile; shift ;;
    -f | --filter) filterExpression="$2"; shift 2 ;;
    *) break ;;
  esac
done

local threeDotsHtml="$( < "$WEB_THREEDOTS_TEMPLATE_FILE" )"
threeDotsHtml="${threeDotsHtml//&/\\&}"
sed -i "s|\\\threedots|$threeDotsHtml|" "$FULL_SOURCE_FILE"
sed -i "s|\\\mainmatter||" "$FULL_SOURCE_FILE"
sed -i "s|\\\frontmatter||" "$FULL_SOURCE_FILE"
sed -i "s|\\\pagebreak||" "$FULL_SOURCE_FILE"

FN source metadata load

FN log bold green "Generating website"

PARENT navigation init

local name=
for part in "${!PART_TITLES[@]}"
do
  name="${PART_TITLES[$part]}"
  [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log green "• ${PART_TITLES[$part]}"
  [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && PARENT pages generate part "$part"

  for chapter in ${PART_CHAPTERS[$part]}
  do
    name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]}"
    [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log light green "  ◦ ${CHAPTER_TITLES[$chapter]}"
    [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && PARENT pages generate chapter "$part" "$chapter"

    for section in ${CHAPTER_SECTIONS[$chapter]}
    do
      name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]}"
      [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log green "    • ${SECTION_TITLES[$section]}"
      [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && PARENT pages generate section "$part" "$chapter" "$section"

      # for subSection in ${SECTION_SUB_SECTIONS[$section]}
      # do
      #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]}"
      #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log light green "      ◦ ${SUB_SECTION_TITLES[$subSection]}"

      #   # for smallSection in ${SUB_SECTION_SMALL_SECTIONS[*]}
      #   # do
      #   #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]} ${SMALL_SECTION_TITLES[$smallSection]}"
      #   #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log green "        • ${SMALL_SECTION_TITLES[$smallSection]}"

      #   #   # for minorSection in ${SMALL_SECTION_MINOR_SECTIONS[*]}
      #   #   # do
      #   #   #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]} ${SMALL_SECTION_TITLES[$smallSection]} ${MINOR_SECTION_TITLES[$minorSection]}"
      #   #   #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && FN log light green "          ◦ ${MINOR_SECTION_TITLES[$minorSection]}"
      #   #   # done
      #   # done
      # done
    done
  done
done