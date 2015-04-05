function MetaBuilders_RowSelectorColumn_SelectAll( parentCheckBox ) {
    if ( typeof( document.getElementById ) == "undefined" ) return;
    if ( parentCheckBox == null || typeof( parentCheckBox.participants ) == "undefined" ) {
        return;
    }
    var participants = parentCheckBox.participants;
    for ( var i=0; i < participants.length; i++ ) {
        var participant = participants[i];
        if ( participant != null ) {
            participant.checked = parentCheckBox.checked;
        }
    }
}
function MetaBuilders_RowSelectorColumn_Register( parentName, childName ) {
    if ( typeof( document.getElementById ) == "undefined" ) return;
    var parent = document.getElementById( parentName );
    var child = document.getElementById( childName );
    if ( parent == null || child == null ) {
        return;
    }
    if ( typeof( parent.participants ) == "undefined" ) {
        parent.participants = new Array();
    }
    parent.participants[parent.participants.length] = child;
}
function MetaBuilders_RowSelectorColumn_CheckChildren( parentName ) {
	if ( typeof( document.getElementById ) == "undefined" ) return;
    var parent = document.getElementById( parentName );
    if ( parent == null || typeof( parent.participants ) == "undefined" ) return;
    var participants = parent.participants;
    for ( var i=0; i < participants.length; i++ ) {
        var participant = participants[i];
        if ( participant != null && !participant.checked ) {
				parent.checked = false;
				return;
        }
    }
    parent.checked = true;
}
