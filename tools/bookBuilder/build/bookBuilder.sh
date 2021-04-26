#! /usr/bin/env bash

# Book 'Metadata' - Structured Data representing the parts, chapters, sections, etc
# When loaded via `source metadata load`, is shared across function invocations
declare -a PART_TITLES=()      CHAPTER_TITLES=()      SECTION_TITLES=()       SUB_SECTION_TITLES=()         SMALL_SECTION_TITLES=()         MINOR_SECTION_TITLES=()      \
           PART_CHAPTERS=()    CHAPTER_SECTIONS    SECTION_SUB_SECTIONS SUB_SECTION_SMALL_SECTIONS SMALL_SECTION_MINOR_SECTIONS                           \
           PART_START_LINES=() CHAPTER_START_LINES=() SECTION_START_LINES=()  SUB_SECTION_START_LINES=()    SMALL_SECTION_START_LINES=()    MINOR_SECTION_START_LINES=() \
           PART_END_LINES=()   CHAPTER_END_LINES=()   SECTION_END_LINES=()    SUB_SECTION_END_LINES=()      SMALL_SECTION_START_LINES=()    MINOR_SECTION_END_LINES=()   \
           PART_FILE_PATHS=()  CHAPTER_FILE_PATHS=()  SECTION_FILE_PATHS=()   SUB_SECTION_FILE_PATHS=()     SMALL_SECTION_START_LINES=()    MINOR_SECTION_FILE_PATHS=()   \
           PART_URLS=()        CHAPTER_URLS=()        SECTION_URLS=()         SUB_SECTION_URLS=()           SMALL_SECTION_START_LINES=()    MINOR_SECTION_URLS=()         \

## @command bookBuilder
bookBuilder() {
  declare -a __bookBuilder__mainCliCommands=("bookBuilder")
  declare -a __bookBuilder__originalCliCommands=("$@")

local -r BOOK="How-to Write a Testing Framework"
local -r AUTHOR="Rebecca Taylor"

local -r NEWLINE=$'\n'
local -r HR="${NEWLINE}${NEWLINE}---${NEWLINE}${NEWLINE}"

local -r SOURCE_DIR=chapters
local -r TEMPLATE_DIR=templates
local -r WEBSITE_DIR=docs

local -r FULL_SOURCE_DIR=source
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

  local __bookBuilder__mainCliCommandDepth="1"
  __bookBuilder__mainCliCommands+=("$1")
  local __bookBuilder__mainCliCommands_command1="$1"
  shift
  case "$__bookBuilder__mainCliCommands_command1" in
    "epub")
    ## @command bookBuilder epub
      local __bookBuilder__mainCliCommandDepth="2"
      __bookBuilder__mainCliCommands+=("$1")
      local __bookBuilder__mainCliCommands_command2="$1"
      shift
      case "$__bookBuilder__mainCliCommands_command2" in
        "generate")
        ## @command bookBuilder epub generate
          
          bookBuilder log bold green "Generating ePub"
          
          cp "$EPUB_FULL_SOURCE_HEADER_FILE" "$EPUB_FULL_SOURCE_FILE"
          echo >> "$EPUB_FULL_SOURCE_FILE"
          cat "$FULL_SOURCE_FILE" >> "$EPUB_FULL_SOURCE_FILE"
          
          pandoc                                    \
            --css "$EPUB_CSS_FILE"                  \
            --toc                                   \
            --toc-depth=3                           \
            --listings                              \
            --top-level-division=chapter            \
            --highlight-style pygments              \
            -H "$PDF_LATEX_PREAMBLE_FILE"           \
            -V classoption=oneside                  \
            -V linkcolor:blue                       \
            -f markdown+raw_tex                     \
            -o "$EPUB_OUTPUT_FILE"                   \
            "$EPUB_FULL_SOURCE_FILE"
        ## @
  
            ;;
        *)
          echo "Unknown 'bookBuilder epub' command: $__bookBuilder__mainCliCommands_command2" >&2
          return 1
          ;;
      esac
    ## @

        ;;
    "error")
    ## @command bookBuilder error
      if [[ "$1" = *'%'* ]]
      then
        local format="$1"
        shift
        bookBuilder log bold red "%s $format" "[Error]" "$@" >&2
      else
        bookBuilder log bold red "[Error]" "$@" >&2
      fi
      exit 1
    ## @

        ;;
    "log")
    ## @command bookBuilder log
      [ "$VERBOSE" = true ] || return
       
      local usePrintf=
      local printfFormat=
      local colorName=
      local colorNumber=
      local fontBold=
      local fontLight=
      local fontWeight=
      
      while true
      do
        case "$1" in
          red | green | yellow | blue | magenta | cyan | gray | grey) colorName="$1"; shift ;;
          bold) fontBold=true; shift; ;;
          light) fontLight=true; shift; ;;
          *'%'*) usePrintf=true; printfFormat="$1"; shift ;;
          *) break ;;
        esac
      done
      
      if [ -n "$colorName" ]
      then
        case "$colorName" in
          red) colorNumber=31 ;;
          green) colorNumber=32 ;;
          yellow) colorNumber=33 ;;
          blue) colorNumber=34 ;;
          magenta) colorNumber=35 ;;
          cyan) colorNumber=36 ;;
          gray | grey) colorNumber=37 ;;
        esac
      fi
      
      if [ -n "$fontBold" ]
      then
        fontWeight=";1"
        [ -z "$colorNumber" ] && colorNumber=39
      fi
      
      [ -n "$fontLight" ] && let colorNumber="colorNumber + 60"
      
      if [ -n "$colorNumber" ]
      then
        if [ -n "$usePrintf" ]
        then
          printf "\e[$colorNumber${fontWeight}m$printfFormat\e[0m" "$@"
        else
          echo -e "\e[$colorNumber${fontWeight}m$*\e[0m"
        fi
      else
        if [ -n "$usePrintf" ]
        then
          printf "$printfFormat" "$@"
        else
          echo "$@"
        fi
      fi
    ## @

        ;;
    "pdf")
    ## @command bookBuilder pdf
      local __bookBuilder__mainCliCommandDepth="2"
      __bookBuilder__mainCliCommands+=("$1")
      local __bookBuilder__mainCliCommands_command2="$1"
      shift
      case "$__bookBuilder__mainCliCommands_command2" in
        "generate")
        ## @command bookBuilder pdf generate
          bookBuilder log bold cyan "Generating PDF"
          
          local pdfSourceHeader="$PDF_FULL_SOURCE_HEADER_NO_FRONTBACK_FILE"
          local pdfLatexFile="$PDF_LATEX_SIMPLE_TEMPLATE_FILE"
          
          local filterExpression=
          while (( $# > 0 ))
          do
            case "$1" in
              -c | --compile) bookBuilder source recompile; shift ;;
              -f | --filter) filterExpression="$2"; shift 2 ;;
              -i | --image | --cover-image)
                shift
                pdfSourceHeader="$PDF_FULL_SOURCE_HEADER_FILE"
                pdfLatexFile="$PDF_LATEX_COVER_IMAGE_TEMPLATE_FILE"
                ;;
              *) break ;;
            esac
          done
          
          bookBuilder pdf updateTemplate "$pdfLatexFile"
          
          cp "$pdfSourceHeader" "$PDF_FULL_SOURCE_FILE"
          echo >> "$PDF_FULL_SOURCE_FILE"
          
          if [ -z "$filterExpression" ]
          then
            cat "$FULL_SOURCE_FILE" >> "$PDF_FULL_SOURCE_FILE"
          else
            : # TODO # LATER
            cat "$FULL_SOURCE_FILE" >> "$PDF_FULL_SOURCE_FILE"
          fi
          
          pandoc                                    \
            --toc                                   \
            --toc-depth=3                           \
            --listings                              \
            --template "$PDF_LATEX_TEMPLATE_NAME"   \
            --top-level-division=chapter            \
            --highlight-style pygments              \
            --pdf-engine xelatex                    \
            -H "$PDF_LATEX_PREAMBLE_FILE"           \
            -V classoption=oneside                  \
            -V linkcolor:blue                       \
            -f markdown+raw_tex                     \
            -o "$PDF_OUTPUT_FILE"                   \
            "$PDF_FULL_SOURCE_FILE"
        ## @
  
            ;;
        "installDependencies")
        ## @command bookBuilder pdf installDependencies
          sudo apt install pandoc ttf-dejavu-extra librsvg2-bin texlive-full
        ## @
  
            ;;
        "updateTemplate")
        ## @command bookBuilder pdf updateTemplate
          bookBuilder log bold cyan "- Updating installed $PDF_LATEX_TEMPLATE_NAME template"
          
          mkdir -p "$PDF_PANDOC_TEMPLATE_DIRECTORY"
          if (( $# > 0 ))
          then
            bookBuilder log light cyan "- $1 --> $PDF_PANDOC_TEMPLATE_DIRECTORY/$PDF_LATEX_TEMPLATE_NAME.latex"
            cp "$1" "$PDF_PANDOC_TEMPLATE_DIRECTORY/$PDF_LATEX_TEMPLATE_NAME.latex"
          else
            bookBuilder log light cyan "- $PDF_LATEX_SIMPLE_TEMPLATE_FILE --> $PDF_PANDOC_TEMPLATE_DIRECTORY/$PDF_LATEX_TEMPLATE_NAME.latex"
            cp "$PDF_LATEX_SIMPLE_TEMPLATE_FILE" "$PDF_PANDOC_TEMPLATE_DIRECTORY/$PDF_LATEX_TEMPLATE_NAME.latex"
          fi
        ## @
  
            ;;
        *)
          echo "Unknown 'bookBuilder pdf' command: $__bookBuilder__mainCliCommands_command2" >&2
          return 1
          ;;
      esac
    ## @

        ;;
    "source")
    ## @command bookBuilder source
      local __bookBuilder__mainCliCommandDepth="2"
      __bookBuilder__mainCliCommands+=("$1")
      local __bookBuilder__mainCliCommands_command2="$1"
      shift
      case "$__bookBuilder__mainCliCommands_command2" in
        "compile")
        ## @command bookBuilder source compile
          bookBuilder log bold blue "Compile $SOURCE_DIR --> $FULL_SOURCE_FILE"
          
          echo > "$FULL_SOURCE_FILE" # Start fresh combined source file
          
          local file
          while read -d '' file; do
            echo >> "$FULL_SOURCE_FILE"
            cat "$file" >> "$FULL_SOURCE_FILE"
            echo >> "$FULL_SOURCE_FILE"
            bookBuilder log light blue "- $file"
          done < <( find "$SOURCE_DIR" -type f -name '*.md' -print0 | sort -z )
        ## @
  
            ;;
        "metadata")
        ## @command bookBuilder source metadata
            local __bookBuilder__mainCliCommandDepth="3"
            __bookBuilder__mainCliCommands+=("$1")
            local __bookBuilder__mainCliCommands_command3="$1"
            shift
            case "$__bookBuilder__mainCliCommands_command3" in
              "clear")
              ## @command bookBuilder source metadata clear
                # Reset locals (strings and integers)
                declare part=          chapter=          section=          subSection=          smallSection=          minorSection=          \
                        partTitle=     chapterTitle=     sectionTitle=     subSectionTitle=     smallSectionTitle=     minorSectionTitle      \
                        partStartLine= chapterStartLine= sectionStartLine= subSectionStartLine= smallSectionStartLine= minorSectionStartLine= \
                        partEndLine=   chapterEndLine=   sectionEndLine=   subSectionEndLine=   smallSectionEndLine=   minorSectionEndLine=
                        partContent=   chapterContent=   sectionContent=   subSectionContent=   smallSectionContent=   minorSectionContent=
                
                # Reset file globals (arrays)
                PART_TITLES=()       CHAPTER_TITLES=()       SECTION_TITLES=()        SUB_SECTION_TITLES=()          SMALL_SECTION_TITLES=()          MINOR_SECTION_TITLES=()
                PART_CHAPTERS=()     CHAPTER_SECTIONS=()     SECTION_SUB_SECTIONS=()  SUB_SECTION_SMALL_SECTIONS=()  SMALL_SECTION_MINOR_SECTIONS=()
                PART_START_LINES=()  CHAPTER_START_LINES=()  SECTION_START_LINES=()   SUB_SECTION_START_LINES=()     SMALL_SECTION_START_LINES=()     MINOR_SECTION_START_LINES=()
                PART_END_LINES=()    CHAPTER_END_LINES=()    SECTION_END_LINES=()     SUB_SECTION_END_LINES=()       SMALL_SECTION_START_LINES=()     MINOR_SECTION_END_LINES=() 
              ## @
      
                  ;;
              "generate")
              ## @command bookBuilder source metadata generate
                [ -f "$FULL_SOURCE_FILE" ] || bookBuilder error '%s %s\n' "Cannot generate metadata" "$FULL_SOURCE_FILE not found"
                
                bookBuilder log bold magenta "Generating metadata from $FULL_SOURCE_FILE"
                
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
                        partSafeTitle="$( bookBuilder web getSafeTitle "$partTitle" )"
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
                        chapterSafeTitle="$( bookBuilder web getSafeTitle "$chapterTitle" )"
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
                        sectionSafeTitle="$( bookBuilder web getSafeTitle "$sectionTitle" )"
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
                        subSectionSafeTitle="$( bookBuilder web getSafeTitle "$subSectionTitle" )"
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
                        smallSectionSafeTitle="$( bookBuilder web getSafeTitle "$smallSectionTitle" )"
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
                        minorSectionSafeTitle="$( bookBuilder web getSafeTitle "$minorSectionTitle" )"
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
              ## @
      
                  ;;
              "load")
              ## @command bookBuilder source metadata load
                bookBuilder log bold yellow "Load Book Metadata"
                
                if (( ${#CHAPTER_TITLES[@]} > 0 ))
                then
                  bookBuilder log light yellow "- Already loaded"
                  return
                fi
                
                if [ -f "$FULL_SOURCE_METADATA_FILE" ]
                then
                  bookBuilder log light yellow "- Loaded existing metadata $FULL_SOURCE_METADATA_FILE"
                  source "$FULL_SOURCE_METADATA_FILE"
                  return
                fi
                
                bookBuilder source metadata generate
                
                if [ -f "$FULL_SOURCE_METADATA_FILE" ]
                then
                  bookBuilder log light yellow "- Loaded metadata $FULL_SOURCE_METADATA_FILE"
                  source "$FULL_SOURCE_METADATA_FILE"
                  return
                else
                  bookBuilder error "Metadata was not generated"
                fi
              ## @
      
                  ;;
              "regenerate")
              ## @command bookBuilder source metadata regenerate
                bookBuilder source metadata clear
                bookBuilder source metadata generate
                bookBuilder source metadata save
              ## @
      
                  ;;
              "reload")
              ## @command bookBuilder source metadata reload
                bookBuilder source metadata clear
                bookBuilder source metadata load
              ## @
      
                  ;;
              "save")
              ## @command bookBuilder source metadata save
                echo > "$FULL_SOURCE_METADATA_FILE" # Start fresh metadata file
                
                local variable
                for variable in PART_TITLES      CHAPTER_TITLES      SECTION_TITLES       SUB_SECTION_TITLES         SMALL_SECTION_TITLES         MINOR_SECTION_TITLES      \
                                PART_CHAPTERS    CHAPTER_SECTIONS    SECTION_SUB_SECTIONS SUB_SECTION_SMALL_SECTIONS SMALL_SECTION_MINOR_SECTIONS                           \
                                PART_START_LINES CHAPTER_START_LINES SECTION_START_LINES  SUB_SECTION_START_LINES    SMALL_SECTION_START_LINES    MINOR_SECTION_START_LINES \
                                PART_END_LINES   CHAPTER_END_LINES   SECTION_END_LINES    SUB_SECTION_END_LINES      SMALL_SECTION_START_LINES    MINOR_SECTION_END_LINES   \
                                PART_FILE_PATHS  CHAPTER_FILE_PATHS  SECTION_FILE_PATHS   SUB_SECTION_FILE_PATHS     SMALL_SECTION_START_LINES    MINOR_SECTION_FILE_PATHS  \
                                PART_URLS        CHAPTER_URLS        SECTION_URLS         SUB_SECTION_URLS           SMALL_SECTION_START_LINES    MINOR_SECTION_URLS
                do
                  declare -p "$variable" | sed 's/^declare [^ ]* //' >> "$FULL_SOURCE_METADATA_FILE"
                done
                
                bookBuilder log light magenta "- Metadata saved to $FULL_SOURCE_METADATA_FILE"
              ## @
      
                  ;;
              *)
                echo "Unknown 'bookBuilder source metadata' command: $__bookBuilder__mainCliCommands_command3" >&2
                return 1
                ;;
            esac
        ## @
  
            ;;
        "recompile")
        ## @command bookBuilder source recompile
          bookBuilder source compile
          bookBuilder source metadata regenerate
        ## @
  
            ;;
        *)
          echo "Unknown 'bookBuilder source' command: $__bookBuilder__mainCliCommands_command2" >&2
          return 1
          ;;
      esac
    ## @

        ;;
    "web")
    ## @command bookBuilder web
      local __bookBuilder__mainCliCommandDepth="2"
      __bookBuilder__mainCliCommands+=("$1")
      local __bookBuilder__mainCliCommands_command2="$1"
      shift
      case "$__bookBuilder__mainCliCommands_command2" in
        "generate")
        ## @command bookBuilder web generate
          local filterExpression=
          
          while (( $# > 0 ))
          do
            case "$1" in
              -c | --compile) bookBuilder source recompile; shift ;;
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
          
          bookBuilder source metadata load
          
          bookBuilder log bold green "Generating website"
          
          bookBuilder web navigation init
          
          local name=
          for part in "${!PART_TITLES[@]}"
          do
            name="${PART_TITLES[$part]}"
            [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log green "• ${PART_TITLES[$part]}"
            [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder web pages generate part "$part"
          
            for chapter in ${PART_CHAPTERS[$part]}
            do
              name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]}"
              [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log light green "  ◦ ${CHAPTER_TITLES[$chapter]}"
              [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder web pages generate chapter "$part" "$chapter"
          
              for section in ${CHAPTER_SECTIONS[$chapter]}
              do
                name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]}"
                [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log green "    • ${SECTION_TITLES[$section]}"
                [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder web pages generate section "$part" "$chapter" "$section"
          
                # for subSection in ${SECTION_SUB_SECTIONS[$section]}
                # do
                #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]}"
                #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log light green "      ◦ ${SUB_SECTION_TITLES[$subSection]}"
          
                #   # for smallSection in ${SUB_SECTION_SMALL_SECTIONS[*]}
                #   # do
                #   #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]} ${SMALL_SECTION_TITLES[$smallSection]}"
                #   #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log green "        • ${SMALL_SECTION_TITLES[$smallSection]}"
          
                #   #   # for minorSection in ${SMALL_SECTION_MINOR_SECTIONS[*]}
                #   #   # do
                #   #   #   name="${PART_TITLES[$part]} ${CHAPTER_TITLES[$chapter]} ${SECTION_TITLES[$section]} ${SUB_SECTION_TITLES[$subSection]} ${SMALL_SECTION_TITLES[$smallSection]} ${MINOR_SECTION_TITLES[$minorSection]}"
                #   #   #   [ -z "$filterExpression" ] || [[ "$name" =~ $filterExpression ]] && bookBuilder log light green "          ◦ ${MINOR_SECTION_TITLES[$minorSection]}"
                #   #   # done
                #   # done
                # done
              done
            done
          done
        ## @
  
            ;;
        "getChapterUrl")
        ## @command bookBuilder web getChapterUrl
          local chapter="$1"
          bookBuilder web load chapter
          printf '%s' "$chapterUrl"
        ## @
  
            ;;
        "getPartUrl")
        ## @command bookBuilder web getPartUrl
          local part="$1"
          bookBuilder web load part
          printf '%s' "$partUrl"
        ## @
  
            ;;
        "getSafeTitle")
        ## @command bookBuilder web getSafeTitle
          local text="$1"
          text="${text//&&/AND}"
          text="${text//||/OR}"
          text="${text/\[\[/Brackets}"
          text="${text/\(\(/Parentheses}"
          text="${text/\'/}"
          text="${text//[^a-zA-Z0-9_]/_}"
          text="${text//__/_}"
          text="${text//__/_}"
          text="${text//__/_}"
          text="${text#_}"
          text="${text%_}"
          printf '%s' "$text"
        ## @
  
            ;;
        "getSectionUrl")
        ## @command bookBuilder web getSectionUrl
          local section="$1"
          bookBuilder web load section
          printf '%s' "$sectionUrl"
        ## @
  
            ;;
        "navigation")
        ## @command bookBuilder web navigation
            local __bookBuilder__mainCliCommandDepth="3"
            __bookBuilder__mainCliCommands+=("$1")
            local __bookBuilder__mainCliCommands_command3="$1"
            shift
            case "$__bookBuilder__mainCliCommands_command3" in
              "addBackButton")
              ## @command bookBuilder web navigation addBackButton
                (( $# == 0 )) && bookBuilder error "bookBuilder web navigation addBackButton requires 1-2 arguments: [title] [url (optional)]"
                
                local titleTemplate="$( < "$WEB_NAVIGATION_TITLE_TEMPLATE_FILE" )"
                titleTemplate="${titleTemplate/TITLE-TEXT/<i class=\'fas fa-arrow-alt-circle-left\'></i>&nbsp; $1}"
                titleTemplate="${titleTemplate/TITLE-URL/${2:-\#}}"
                
                printf '%s' "$titleTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
              ## @
      
                  ;;
              "addItem")
              ## @command bookBuilder web navigation addItem
                (( $# == 0 )) && bookBuilder error "bookBuilder web navigation addTitle requires 1-2 arguments: [item] [url (optional)]"
                
                local text="$1"
                local safeText=''
                
                # Replace ` with <code> blocks
                local codeBlockOpen=
                local -i i
                for (( i = 0; i < ${#text}; i++ ))
                do
                  if [ "${text:$i:1}" = '`' ]
                  then
                    if [ -z "$codeBlockOpen" ]
                    then
                      codeBlockOpen=true
                      safeText+="<code>"
                    else
                      codeBlockOpen=
                      safeText+="</code>"
                    fi
                  else
                    safeText+="${text:$i:1}"
                  fi
                done
                
                local itemTemplate="$( < "$WEB_NAVIGATION_ITEM_TEMPLATE_FILE" )"
                itemTemplate="${itemTemplate/ITEM-TEXT/$safeText}"
                itemTemplate="${itemTemplate/ITEM-URL/${2:-\#}}"
                
                printf '%s' "$itemTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
              ## @
      
                  ;;
              "addTitle")
              ## @command bookBuilder web navigation addTitle
                (( $# == 0 )) && bookBuilder error "bookBuilder web navigation addTitle requires 1-2 arguments: [title] [url (optional)]"
                
                local titleTemplate="$( < "$WEB_NAVIGATION_TITLE_TEMPLATE_FILE" )"
                titleTemplate="${titleTemplate/TITLE-TEXT/$1}"
                titleTemplate="${titleTemplate/TITLE-URL/${2:-\#}}"
                
                printf '%s' "$titleTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
              ## @
      
                  ;;
              "init")
              ## @command bookBuilder web navigation init
                cp "$WEB_NAVIGATION_ROOT_TEMPLATE_FILE" "$WEBSITE_NAVIGATION_FILE"
              ## @
      
                  ;;
              "newSidebar")
              ## @command bookBuilder web navigation newSidebar
                (( $# == 0 )) && bookBuilder error "bookBuilder web navigation newSidebar requires 1 argument: [sidebar name]"
                
                local sidebarTemplate="$( < "$WEB_NAVIGATION_SIDEBAR_TEMPLATE_FILE" )"
                sidebarTemplate="${sidebarTemplate//SIDEBAR_NAME/$1}"
                
                printf '%s' "$sidebarTemplate${NEWLINE}" >> "$WEBSITE_NAVIGATION_FILE"
              ## @
      
                  ;;
              *)
                echo "Unknown 'bookBuilder web navigation' command: $__bookBuilder__mainCliCommands_command3" >&2
                return 1
                ;;
            esac
        ## @
  
            ;;
        "pages")
        ## @command bookBuilder web pages
            local __bookBuilder__mainCliCommandDepth="3"
            __bookBuilder__mainCliCommands+=("$1")
            local __bookBuilder__mainCliCommands_command3="$1"
            shift
            case "$__bookBuilder__mainCliCommands_command3" in
              "appendNextLink")
              ## @command bookBuilder web pages appendNextLink
                local nextTemplate="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
                nextTemplate="${nextTemplate//NEXT-TEXT/$1}"
                nextTemplate="${nextTemplate//NEXT-URL/$2}"
                
                printf '%s' "$nextTemplate"
              ## @
      
                  ;;
              "appendPreviousLink")
              ## @command bookBuilder web pages appendPreviousLink
                local previousText="$1"
                local previousUrl="$2"
                
                local previousTemplate="$( < "$WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE" )"
                previousTemplate="${previousTemplate//PREVIOUS-TEXT/$previousText}"
                previousTemplate="${previousTemplate//PREVIOUS-URL/$previousUrl}"
                
                pageContent+="${NEWLINE}$previousTemplate${NEWLINE}"
              ## @
      
                  ;;
              "appendPreviousNextLinks")
              ## @command bookBuilder web pages appendPreviousNextLinks
                [ -z "$previousText" ] || [ -z "$previousUrl" ] || [ -z "$nextText" ] || [ -z "$nextUrl" ] && bookBuilder error "bookBuilder web pages appendPreviousNextLinks requires configuring \$previousText \$previousUrl \$nextText \$nextUrl"
                
                local prevNextTemplate="$( < "$WEB_PAGE_NEXT_PREVIOUS_LINKS_TEMPLATE_FILE" )"
                prevNextTemplate="${prevNextTemplate//NEXT-TEXT/$nextText}"
                prevNextTemplate="${prevNextTemplate//NEXT-URL/$nextUrl}"
                prevNextTemplate="${prevNextTemplate//PREVIOUS-TEXT/$previousText}"
                prevNextTemplate="${prevNextTemplate//PREVIOUS-URL/$previousUrl}"
                
                pageContent+="${NEWLINE}$prevNextTemplate${NEWLINE}"
              ## @
      
                  ;;
              "generate")
              ## @command bookBuilder web pages generate
                    local __bookBuilder__mainCliCommandDepth="4"
                    __bookBuilder__mainCliCommands+=("$1")
                    local __bookBuilder__mainCliCommands_command4="$1"
                    shift
                    case "$__bookBuilder__mainCliCommands_command4" in
                      "chapter")
                      ## @command bookBuilder web pages generate chapter
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
                        
                        bookBuilder web navigation newSidebar "$navigation"
                        
                        local bookPart
                        for bookPart in "${!PART_TITLES[@]}"
                        do
                          if (( bookPart == part ))
                          then
                            bookBuilder web navigation addTitle "<strong>${PART_TITLES[$part]}</strong>" "${PART_URLS[$part]}"
                          else
                            bookBuilder web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
                          fi
                        
                          local foundChapter=
                          local nextChapter=
                          local partChapter
                          for partChapter in ${PART_CHAPTERS[$bookPart]}
                          do
                            if (( partChapter == chapter ))
                            then
                              foundChapter=true
                              bookBuilder web navigation addTitle "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
                              local chapterSection
                              for chapterSection in "${sections[@]}"
                              do
                                bookBuilder web navigation addItem "» ${SECTION_TITLES[$chapterSection]}" "${SECTION_URLS[$chapterSection]}"
                              done
                            else
                              [ "$foundChapter" = true ] && [ -z "$nextChapter" ] && nextChapter="$partChapter"
                              (( bookPart == part )) && bookBuilder web navigation addTitle "<i>• ${CHAPTER_TITLES[$partChapter]}</i>" "${CHAPTER_URLS[$partChapter]}"
                            fi
                          done
                        done
                        
                        page+="$( bookBuilder web pages generate frontMatter "${CHAPTER_TITLES[$chapter]}" "${CHAPTER_URLS[$chapter]}" "$navigation" )${NEWLINE}"
                        
                        page+="<h1><a href=\"${PART_URLS[part]}\">${PART_TITLES[part]}</a></h1>${NEWLINE}${NEWLINE}"
                        page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )"
                        page+="${HR}"
                        
                        if (( ${#sections[@]} > 0 ))
                        then
                          page+="$( bookBuilder web pages generate nextLink "${SECTION_TITLES[${sections[0]}]}" "${SECTION_URLS[${sections[0]}]}" )"
                        elif [ -n "$nextChapter" ]
                        then
                          page+="$( bookBuilder web pages generate nextLink "${CHAPTER_TITLES[$nextChapter]}" "${CHAPTER_URLS[$nextChapter]}" )"
                        elif (( part < ${#PART_TITLES[@]} - 1 ))
                        then
                          page+="$( bookBuilder web pages generate nextLink "${PART_TITLES[$(( part + 1 ))]}" "${PART_URLS[$(( part + 1 ))]}" )"
                        fi
                        
                        if (( chapter == ${chapters[0]} )) && (( part > 0 ))
                        then
                          page+="$( bookBuilder web pages generate previousLink "${PART_TITLES[part]}" "${PART_URLS[part]}" )"
                        else
                          page+="$( bookBuilder web pages generate previousLink "${CHAPTER_TITLES[$(( chapter - 1 ))]}" "${CHAPTER_URLS[$(( chapter - 1 ))]}" )"
                        fi
                        
                        mkdir -p "${CHAPTER_FILE_PATHS[$chapter]%/*}"
                        
                        printf '%s' "$page" > "${CHAPTER_FILE_PATHS[$chapter]}"
                      ## @
            
                          ;;
                      "frontMatter")
                      ## @command bookBuilder web pages generate frontMatter
                        local pageFrontMatter="$( < "$WEB_PAGE_FRONTMATTER_TEMPLATE_FILE" )"
                        pageFrontMatter="${pageFrontMatter//TITLE/$1}"
                        pageFrontMatter="${pageFrontMatter//PERMALINK/$2}"
                        pageFrontMatter="${pageFrontMatter//NAV/${3:-none}}"
                        printf '%s' "$pageFrontMatter"
                      ## @
            
                          ;;
                      "nextLink")
                      ## @command bookBuilder web pages generate nextLink
                        local template="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
                        template="${template//NEXT-TEXT/$1}"
                        template="${template//NEXT-URL/$2}"
                        printf '%s' "$template"
                      ## @
            
                          ;;
                      "part")
                      ## @command bookBuilder web pages generate part
                        local page=''
                        local part=$1
                        local -a chapters
                        read -ra chapters <<< "${PART_CHAPTERS[$part]}"
                        local startLine="${PART_START_LINES[$part]}"
                        local endLine="${PART_END_LINES[$part]}"
                        (( ${#chapters[@]} > 0 )) && (( endLine = ${CHAPTER_START_LINES[${chapters[0]}]} - 1 ))
                        
                        local navigation="${PART_URLS[$part]//[^a-zA-Z0-9_]/}"
                        
                        bookBuilder web navigation newSidebar "$navigation"
                        
                        local bookPart
                        for bookPart in "${!PART_TITLES[@]}"
                        do
                          if (( bookPart == part ))
                          then
                            bookBuilder web navigation addTitle "${PART_TITLES[$bookPart]}" "${PART_URLS[$bookPart]}"
                            local partChapter
                            for partChapter in ${PART_CHAPTERS[$bookPart]}
                            do
                              bookBuilder web navigation addItem "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
                            done
                          else
                            bookBuilder web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
                          fi
                        done
                        
                        page+="$( bookBuilder web pages generate frontMatter "${PART_TITLES[$part]}" "${PART_URLS[$part]}" "$navigation" )${NEWLINE}"
                        page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )"
                        page+="${HR}"
                        if (( ${#chapters[@]} > 0 ))
                        then
                          (( part == ${#PART_TITLES[@]} - 1 )) || page+="$( bookBuilder web pages generate nextLink "${CHAPTER_TITLES[${chapters[0]}]}" "${CHAPTER_URLS[${chapters[0]}]}" )"
                        else
                          (( part == ${#PART_TITLES[@]} - 1 )) || page+="$( bookBuilder web pages generate nextLink "${PART_TITLES[$(( part + 1 ))]}" "${PART_URLS[$(( part + 1 ))]}" )"
                        fi
                        (( part != 0 )) && page+="$( bookBuilder web pages generate previousLink "${PART_TITLES[$(( part - 1 ))]}" "${PART_URLS[$(( part - 1 ))]}" )"
                        
                        mkdir -p "${PART_FILE_PATHS[$part]%/*}"
                        
                        printf '%s' "$page" > "${PART_FILE_PATHS[$part]}"
                      ## @
            
                          ;;
                      "previousLink")
                      ## @command bookBuilder web pages generate previousLink
                        local template="$( < "$WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE" )"
                        template="${template//PREVIOUS-TEXT/$1}"
                        template="${template//PREVIOUS-URL/$2}"
                        printf '%s' "$template"
                      ## @
            
                          ;;
                      "section")
                      ## @command bookBuilder web pages generate section
                        local page=''
                        local part=$1
                        local chapter="$2"
                        local section="$3"
                        local -a sections
                        read -ra sections <<< "${CHAPTER_SECTIONS[$chapter]}"
                        local startLine="${SECTION_START_LINES[$section]}"
                        local endLine="${SECTION_END_LINES[$section]}"
                        
                        local navigation="${SECTION_URLS[$section]//[^a-zA-Z0-9_]/}"
                        
                        bookBuilder web navigation newSidebar "$navigation"
                        
                        local foundSection=
                        local nextSection=
                        local bookPart
                        for bookPart in "${!PART_TITLES[@]}"
                        do
                          if (( bookPart == part ))
                          then
                            bookBuilder web navigation addTitle "<strong>${PART_TITLES[$part]}</strong>" "${PART_URLS[$part]}"
                          else
                            bookBuilder web navigation addTitle "<em>${PART_TITLES[$bookPart]}</em>" "${PART_URLS[$bookPart]}"
                          fi
                        
                          local partChapter
                          for partChapter in ${PART_CHAPTERS[$bookPart]}
                          do
                            if (( partChapter == chapter ))
                            then
                              bookBuilder web navigation addTitle "• ${CHAPTER_TITLES[$partChapter]}" "${CHAPTER_URLS[$partChapter]}"
                              local chapterSection
                              for chapterSection in "${sections[@]}"
                              do
                                bookBuilder web navigation addItem "» ${SECTION_TITLES[$chapterSection]}" "${SECTION_URLS[$chapterSection]}"
                                if (( chapterSection == section ))
                                then
                                  foundSection=true
                                else
                                  [ "$foundSection" = true ] && [ -z "$nextSection" ] && nextSection="$chapterSection"
                                fi
                              done
                            else
                              (( bookPart == part )) && bookBuilder web navigation addTitle "<i>• ${CHAPTER_TITLES[$partChapter]}</i>" "${CHAPTER_URLS[$partChapter]}"
                            fi
                          done
                        done
                        
                        page+="$( bookBuilder web pages generate frontMatter "${SECTION_TITLES[$section]}" "${SECTION_URLS[$section]}" "$navigation" "$showMenu" )${NEWLINE}${NEWLINE}"
                        
                        page+="<h1><a href=\"${PART_URLS[part]}\">${PART_TITLES[part]}</a></h1>${NEWLINE}${NEWLINE}"
                        page+="<h2><a href=\"${CHAPTER_URLS[chapter]}\">${CHAPTER_TITLES[chapter]}</a></h2>${NEWLINE}${NEWLINE}"
                        
                        page+="$( sed -n "$startLine,${endLine}p;" "$FULL_SOURCE_FILE" )${NEWLINE}"
                        page+="${HR}"
                        
                        if [ -n "$nextSection" ]
                        then
                          page+="$( bookBuilder web pages generate nextLink "${SECTION_TITLES[$nextSection]}" "${SECTION_URLS[$nextSection]}" )"
                        elif (( chapter != ${#CHAPTER_SECTIONS[@]} - 1 ))
                        then
                          page+="$( bookBuilder web pages generate nextLink "${CHAPTER_TITLES[$(( chapter + 1 ))]}" "${CHAPTER_URLS[$(( chapter + 1 ))]}" )"
                        else
                          : # Last Chapter in the book TODO
                        fi
                        
                        if (( section == ${sections[0]} ))
                        then
                          page+="$( bookBuilder web pages generate previousLink "${CHAPTER_TITLES[chapter]}" "${CHAPTER_URLS[chapter]}" )"
                        else
                          page+="$( bookBuilder web pages generate previousLink "${SECTION_TITLES[$(( section - 1 ))]}" "${SECTION_URLS[$(( section - 1 ))]}" )"
                        fi
                        
                        mkdir -p "${SECTION_FILE_PATHS[$section]%/*}"
                        
                        printf '%s' "$page" > "${SECTION_FILE_PATHS[$section]}"
                      ## @
            
                          ;;
                      *)
                        echo "Unknown 'bookBuilder web pages generate' command: $__bookBuilder__mainCliCommands_command4" >&2
                        return 1
                        ;;
                    esac
              ## @
      
                  ;;
              "prependNextLink")
              ## @command bookBuilder web pages prependNextLink
                local nextText="$1"
                local nextUrl="$2"
                
                local nextTemplate="$( < "$WEB_PAGE_NEXT_LINK_TEMPLATE_FILE" )"
                nextTemplate="${nextTemplate//NEXT-TEXT/$nextText}"
                nextTemplate="${nextTemplate//NEXT-URL/$nextUrl}"
                
                pageContent="$nextTemplate<br />${NEWLINE}${NEWLINE}$pageContent"
              ## @
      
                  ;;
              "prependPreviousLink")
              ## @command bookBuilder web pages prependPreviousLink
                local previousText="$1"
                local previousUrl="$2"
                
                local previousTemplate="$( < "$WEB_PAGE_PREVIOUS_LINK_TEMPLATE_FILE" )"
                previousTemplate="${previousTemplate//PREVIOUS-TEXT/$previousText}"
                previousTemplate="${previousTemplate//PREVIOUS-URL/$previousUrl}"
                
                pageContent="$previousTemplate<br />${NEWLINE}${NEWLINE}$pageContent"
              ## @
      
                  ;;
              "prependPreviousNextLinks")
              ## @command bookBuilder web pages prependPreviousNextLinks
                [ -z "$previousText" ] || [ -z "$previousUrl" ] || [ -z "$nextText" ] || [ -z "$nextUrl" ] && bookBuilder error "bookBuilder web pages prependPreviousNextLinks requires configuring \$previousText \$previousUrl \$nextText \$nextUrl"
                
                local prevNextTemplate="$( < "$WEB_PAGE_NEXT_PREVIOUS_LINKS_TEMPLATE_FILE" )"
                prevNextTemplate="${prevNextTemplate//NEXT-TEXT/$nextText}"
                prevNextTemplate="${prevNextTemplate//NEXT-URL/$nextUrl}"
                prevNextTemplate="${prevNextTemplate//PREVIOUS-TEXT/$previousText}"
                prevNextTemplate="${prevNextTemplate//PREVIOUS-URL/$previousUrl}"
                
                pageContent="$prevNextTemplate<br />${NEWLINE}${NEWLINE}$pageContent${NEWLINE}"
              ## @
      
                  ;;
              *)
                echo "Unknown 'bookBuilder web pages' command: $__bookBuilder__mainCliCommands_command3" >&2
                return 1
                ;;
            esac
        ## @
  
            ;;
        *)
          echo "Unknown 'bookBuilder web' command: $__bookBuilder__mainCliCommands_command2" >&2
          return 1
          ;;
      esac
    ## @

        ;;
    *)
      echo "Unknown 'bookBuilder' command: $__bookBuilder__mainCliCommands_command1" >&2
      ;;
  esac

}

[ "${BASH_SOURCE[0]}" = "$0" ] && "bookBuilder" "$@"

