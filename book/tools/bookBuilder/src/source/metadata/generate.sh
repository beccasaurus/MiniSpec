[ -f "$FULL_SOURCE_FILE" ] || FN error '%s %s\n' "Cannot generate metadata" "$FULL_SOURCE_FILE not found"

FN log bold magenta "Generating metadata from $FULL_SOURCE_FILE"

local -i lineNumber=0
local line=
while read line
do
  (( lineNumber++ ))

  if [[ "$line" =~ ^([\#]+)[[:space:]](.*)$ ]]
  then
    # Header
    local -i depth="${#BASH_REMATCH[1]}"
    local title="${BASH_REMATCH[2]}"
    case $depth in
      1)
        # Part
        [ -n "$partTitle" ] && PART_END_LINES+=($(( lineNumber - 1 )))
        partTitle="$title"
        partSafeTitle="$( FN web getSafeTitle "$partTitle" )"
        part="${#PART_TITLES[@]}"
        PART_TITLES+=("$partTitle")
        PART_START_LINES+=($lineNumber)
        PART_CHAPTERS+=('')
        PART_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle.md")
        PART_URLS+=("/$partSafeTitle")
        # NEXT
        # PREV
        [ -n "$chapterTitle"      ] && { CHAPTER_END_LINES+=($(( lineNumber - 1 )));       chapterTitle=;      }
        [ -n "$sectionTitle"      ] && { SECTION_END_LINES+=($(( lineNumber - 1 )));       sectionTitle=;      }
        [ -n "$subSectionTitle"   ] && { SUB_SECTION_END_LINES+=($(( lineNumber - 1 )));   subsectionTitle=;   }
        [ -n "$smallSectionTitle" ] && { SMALL_SECTION_END_LINES+=($(( lineNumber - 1 ))); smallSectionTitle=; }
        [ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }
        ;;
      2)
        # Chapter
        [ -n "$chapterTitle" ] && CHAPTER_END_LINES+=($(( lineNumber - 1 )))
        chapterTitle="$title"
        chapterSafeTitle="$( FN web getSafeTitle "$chapterTitle" )"
        chapter="${#CHAPTER_TITLES[@]}"
        CHAPTER_TITLES+=("$chapterTitle")
        CHAPTER_START_LINES+=($lineNumber)
        CHAPTER_SECTIONS+=('')
        CHAPTER_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle/$chapterSafeTitle.md")
        CHAPTER_URLS+=("/$chapterSafeTitle")
        # NEXT
        # PREV
        PART_CHAPTERS[$part]+="$chapter "
        [ -n "$sectionTitle"      ] && { SECTION_END_LINES+=($(( lineNumber - 1 )));       sectionTitle=;      }
        [ -n "$subSectionTitle"   ] && { SUB_SECTION_END_LINES+=($(( lineNumber - 1 )));   subsectionTitle=;   }
        [ -n "$smallSectionTitle" ] && { SMALL_SECTION_END_LINES+=($(( lineNumber - 1 ))); smallSectionTitle=; }
        [ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }
        ;;
      3)
        # Section
        [ -n "$sectionTitle" ] && SECTION_END_LINES+=($(( lineNumber - 1 )))
        sectionTitle="$title"
        sectionSafeTitle="$( FN web getSafeTitle "$sectionTitle" )"
        section="${#SECTION_TITLES[@]}"
        SECTION_TITLES+=("$sectionTitle")
        SECTION_START_LINES+=($lineNumber)
        SECTION_SUB_SECTIONS+=('')
        SECTION_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle/$chapterSafeTitle/$sectionSafeTitle.md")
        SECTION_URLS+=("/$chapterSafeTitle/$sectionSafeTitle")
        # NEXT
        # PREV
        CHAPTER_SECTIONS[$chapter]+="$section "
        [ -n "$subSectionTitle"   ] && { SUB_SECTION_END_LINES+=($(( lineNumber - 1 )));   subsectionTitle=;   }
        [ -n "$smallSectionTitle" ] && { SMALL_SECTION_END_LINES+=($(( lineNumber - 1 ))); smallSectionTitle=; }
        [ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }
        ;;
      4)
        # Sub Section
        [ -n "$subSectionTitle" ] && SUB_SECTION_END_LINES+=($(( lineNumber - 1 )))
        subSectionTitle="$title"
        subSectionSafeTitle="$( FN web getSafeTitle "$subSectionTitle" )"
        subSection="${#SUB_SECTION_TITLES[@]}"
        SUB_SECTION_TITLES+=("$subSectionTitle")
        SUB_SECTION_START_LINES+=($lineNumber)
        SUB_SECTION_SMALL_SECTIONS+=('')
        SUB_SECTION_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle/$chapterSafeTitle/$sectionSafeTitle/$subSectionSafeTitle.md")
        SUB_SECTION_URLS+=("/$chapterSafeTitle/$sectionSafeTitle#$subSectionSafeTitle")
        # NEXT
        # PREV
        SECTION_SUB_SECTIONS[$section]+="$subSection "
        [ -n "$smallSectionTitle" ] && { SMALL_SECTION_END_LINES+=($(( lineNumber - 1 ))); smallSectionTitle=; }
        [ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }
        ;;
      5)
        # Small Section
        [ -n "$smallSectionTitle" ] && SMALL_SECTION_END_LINES+=($(( lineNumber - 1 )))
        smallSectionTitle="$title"
        smallSectionSafeTitle="$( FN web getSafeTitle "$smallSectionTitle" )"
        smallSection="${#SMALL_SECTION_TITLES[@]}"
        SMALL_SECTION_TITLES+=("$smallSectionTitle")
        SMALL_SECTION_START_LINES+=($lineNumber)
        SMALL_SECTION_MINOR_SECTIONS+=('')
        SMALL_SECTION_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle/$chapterSafeTitle/$sectionSafeTitle/$subSectionSafeTitle/$smallSectionSafeTitle.md")
        SMALL_SECTION_URLS+=("/$chapterSafeTitle/$sectionSafeTitle#$smallSectionSafeTitle")
        # NEXT
        # PREV
        SUB_SECTION_SMALL_SECTIONS[$subSection]+="$smallSection "
        [ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }
        ;;
      6)
        # Minor Section
        [ -n "$minorSectionTitle" ] && MINOR_SECTION_END_LINES+=($(( lineNumber - 1 )))
        minorSectionTitle="$title"
        minorSectionSafeTitle="$( FN web getSafeTitle "$minorSectionTitle" )"
        minorSection="${#MINOR_SECTION_TITLES[@]}"
        MINOR_SECTION_TITLES+=("$minorSectionTitle")
        MINOR_SECTION_START_LINES+=($lineNumber)
        MINOR_SECTION_FILE_PATHS+=("$WEBSITE_BOOK_CONTENT_DIR/$partSafeTitle/$chapterSafeTitle/$sectionSafeTitle/$subSectionSafeTitle/$smallSectionSafeTitle/$minorSectionSafeTitle.md")
        MINOR_SECTION_URLS+=("/$chapterSafeTitle/$sectionSafeTitle#$minorSectionSafeTitle")
        # NEXT
        # PREV
        SMALL_SECTION_MINOR_SECTIONS[$smallSection]+="$minorSection "
        ;;
    esac
  fi
done < "$FULL_SOURCE_FILE"

[ -n "$partTitle"         ] && { PART_END_LINES+=($(( lineNumber - 1 )));          partTitle=;         }
[ -n "$chapterTitle"      ] && { CHAPTER_END_LINES+=($(( lineNumber - 1 )));       chapterTitle=;      }
[ -n "$sectionTitle"      ] && { SECTION_END_LINES+=($(( lineNumber - 1 )));       sectionTitle=;      }
[ -n "$subSectionTitle"   ] && { SUB_SECTION_END_LINES+=($(( lineNumber - 1 )));   subsectionTitle=;   }
[ -n "$smallSectionTitle" ] && { SMALL_SECTION_END_LINES+=($(( lineNumber - 1 ))); smallSectionTitle=; }
[ -n "$minorSectionTitle" ] && { MINOR_SECTION_END_LINES+=($(( lineNumber - 1 ))); minorSectionTitle=; }