import * as alt from "alt";
import * as natives from "natives";


alt.on("__ironCore:bridge:js:showHelpText", (text, milliseconds = 5000) => {
  natives.beginTextCommandDisplayHelp('STRING');
  natives.addTextComponentSubstringKeyboardDisplay(text);
  natives.endTextCommandDisplayHelp(0, 0, 0, milliseconds);
});
