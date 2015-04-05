/*
 * This file is part of fileinput.
 * 
 * fileinput is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * fileinput is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 */

(function(b){b.widget("shimmy.fileinput",{options:{buttonText:"Browse",inputText:""},_create:function(){var a=this,d=a.options;a.fileFile=a.element;a.fileWrapper=b("<div></div>").addClass("fileinput-wrapper ui-widget").hover(function(){a.fileButton.addClass("ui-state-hover")},function(){a.fileButton.removeClass("ui-state-hover ui-state-active")}).bind("mousemove.fileinput",function(c){var e=c.pageX-b(this).offset().left-a.fileFile.width()/1.2;c=c.pageY-b(this).offset().top-a.fileFile.height()/2;a.fileFile.css("top",
c).css("left",e)}).bind("mousedown.fileinput",function(){a.fileButton.addClass("ui-state-active")}).bind("mouseup.fileinput",function(){a.fileButton.removeClass("ui-state-active")});a.fileFile.addClass("fileinput-file").wrap(a.fileWrapper);a.fileInput=b("<span></span>").addClass("fileinput-input ui-state-default ui-widget-content ui-corner-left").text(a._getText()).insertBefore(a.fileFile);a.fileButtonText=b("<span></span>").addClass("fileinput-button-text").text(d.buttonText);a.fileButton=b("<span></span>").addClass("fileinput-button ui-state-default ui-widget-header ui-corner-right").insertAfter(a.fileInput).html(a.fileButtonText);
a.fileFile.bind("change.fileinput mouseout.fileinput",function(){a.fileInput.text(a._getText())}).bind("focusin.fileinput",function(){a.fileButton.addClass("ui-state-hover")}).bind("focusout.fileinput",function(){a.fileButton.removeClass("ui-state-hover")})},_getText:function(){fileValue=this.getValue();inputTextValue=this.options.inputText;return fileValue==""?inputTextValue:fileValue},getValue:function(){return fileValue=this.fileFile.val().replace("C:\\fakepath\\","")},reset:function(){this.fileInput.text(this.options.inputText)},
destroy:function(){this.fileInput.remove();this.fileButton.remove();this.fileButtonText.remove();this.fileFile.removeClass("fileinput-file").unwrap(this.fileWrapper);this.fileWrapper.remove();b.Widget.prototype.destroy.call(this)},_setOption:function(a,d){b.Widget.prototype._setOption.apply(this,arguments);switch(a){case "buttonText":this.fileButtonText.text(d);break;case "inputText":this.fileInput.text(this._getText())}}})})(jQuery);
