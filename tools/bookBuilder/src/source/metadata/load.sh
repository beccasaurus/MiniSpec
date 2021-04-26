FN log bold yellow "Load Book Metadata"

if (( ${#CHAPTER_TITLES[@]} > 0 ))
then
  FN log light yellow "- Already loaded"
  return
fi

if [ -f "$FULL_SOURCE_METADATA_FILE" ]
then
  FN log light yellow "- Loaded existing metadata $FULL_SOURCE_METADATA_FILE"
  source "$FULL_SOURCE_METADATA_FILE"
  return
fi

PARENT generate

if [ -f "$FULL_SOURCE_METADATA_FILE" ]
then
  FN log light yellow "- Loaded metadata $FULL_SOURCE_METADATA_FILE"
  source "$FULL_SOURCE_METADATA_FILE"
  return
else
  FN error "Metadata was not generated"
fi