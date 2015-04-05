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

(function($){

	var wrapperClasses = 'fileinput-wrapper ui-widget',
		inputClasses = 'fileinput-input ui-state-default ui-widget-content ui-corner-left',
		buttonClasses = 'fileinput-button ui-state-default ui-widget-header ui-corner-right',
		buttonTextClasses = 'fileinput-button-text',
		fileClasses = 'fileinput-file',
		hoverClasses = 'ui-state-hover',
		activeClasses = 'ui-state-active',
		stateClasses = hoverClasses + ' ' + activeClasses,
		fakePath = 'C:\\fakepath\\';

	$.widget("shimmy.fileinput", {
		options: {
			buttonText: "Browse",
			inputText: ""
		},

		_create: function(){
			var self = this,
				options = self.options;

			self.fileFile = self.element,
			self.fileWrapper = $('<div></div>')
				.addClass(wrapperClasses)
				.hover(function(){
					self.fileButton.addClass(hoverClasses);
				},function(){
					self.fileButton.removeClass(stateClasses);
				}).bind('mousemove.fileinput',function(e){
					var x = (e.pageX - $(this).offset().left) - (self.fileFile.width() / 1.2);
					var y = (e.pageY - $(this).offset().top) - (self.fileFile.height() / 2);
					self.fileFile.css('top', y).css('left', x);
				}).bind('mousedown.fileinput',function(e){
					self.fileButton.addClass(activeClasses);
				}).bind('mouseup.fileinput',function(e){
					self.fileButton.removeClass(activeClasses);
				}),
			self.fileFile
				.addClass(fileClasses)
				.wrap(self.fileWrapper),
			self.fileInput = $('<span></span>')
				.addClass(inputClasses)
				.text(self._getText())
				.insertBefore(self.fileFile),
			self.fileButtonText = $('<span></span>')
				.addClass(buttonTextClasses)
				.text(options.buttonText)
			self.fileButton = $('<span></span>')
				.addClass(buttonClasses)
				.insertAfter(self.fileInput)
				.html(self.fileButtonText);

			self.fileFile.bind('change.fileinput mouseout.fileinput',function(){
				self.fileInput.text(self._getText());
			}).bind('focusin.fileinput',function(){
				self.fileButton.addClass(hoverClasses);
			}).bind('focusout.fileinput',function(){
				self.fileButton.removeClass(hoverClasses);
			});

		},

		_getText: function(){
			var self = this;
			fileValue = self.getValue();
			inputTextValue = self.options.inputText;

			if(fileValue == ''){
				return inputTextValue;
			}else{
				return fileValue;
			}
		},

		getValue: function(){
			var self = this;
			return fileValue = self.fileFile.val().replace(fakePath,'');
		},

		reset: function() {
			var self = this;
			self.fileInput.text(self.options.inputText);
		},

		destroy: function(){
			var self = this;

			self.fileInput.remove();
			self.fileButton.remove();
			self.fileButtonText.remove();
			self.fileFile.removeClass(fileClasses).unwrap(self.fileWrapper);
			self.fileWrapper.remove();

			$.Widget.prototype.destroy.call( self );
		},

		_setOption: function(option, value){
			var self = this;
			$.Widget.prototype._setOption.apply( self, arguments );
			switch(option){
				case "buttonText":
					self.fileButtonText.text(value);
					break;
				case "inputText":
					self.fileInput.text(self._getText());
					break;
			}
		}

	});

})(jQuery);
