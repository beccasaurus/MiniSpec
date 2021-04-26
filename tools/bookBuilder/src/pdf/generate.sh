FN log bold cyan "Generating PDF"

local pdfSourceHeader="$PDF_FULL_SOURCE_HEADER_NO_FRONTBACK_FILE"
local pdfLatexFile="$PDF_LATEX_SIMPLE_TEMPLATE_FILE"

local filterExpression=
while (( $# > 0 ))
do
  case "$1" in
    -c | --compile) FN source recompile; shift ;;
    -f | --filter) filterExpression="$2"; shift 2 ;;
    -i | --image | --cover-image)
      shift
      pdfSourceHeader="$PDF_FULL_SOURCE_HEADER_FILE"
      pdfLatexFile="$PDF_LATEX_COVER_IMAGE_TEMPLATE_FILE"
      ;;
    *) break ;;
  esac
done

PARENT updateTemplate

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
