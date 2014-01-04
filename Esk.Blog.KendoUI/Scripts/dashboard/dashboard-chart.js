/// <reference path="dashboard-core.js"/>
ESK.namespace("ESK.Blog.Dataviz.DonutProgressChart");

ESK.Blog.Dataviz.DonutProgressChart = (function () {

	var chart = { width:200, height:200, }

	chart.init = function (w, h) { this.width = w; this.height = h }

	chart.create = function (elem, name, seriesName) {
		var self = this;
		$(elem).kendoChart({
			transitions: false,
			chartArea: {
				"background": "transparent",
				"height": self.width,
				"width": self.height,
				"margin": {
					"top": 0,
					"right": 0,
					"bottom": 0,
					"left": 0
				},
				//"border": {
				//	"width": 1,
				//	"dashType": "dashdot",
				//	"color": "#000000"
				//}
			},
			theme: "bootstrap",
			title: {
				"text": name,
				"position": "bottom"
			},
			legend: { "visible": false },
			series: [{ "name": seriesName, "type": "donut", "field": "percentage", "categoryField": "source", "explodeField": "explode", "holeSize": 60, "size": 10 }],
			seriesColors: ["#9de219", "#de2e46", "#f1f1f1"],
			tooltip: { "template": "#= category # (#= series.name #): #= value #%", "visible": true },
			dataSource: [{ "source": "Progress", "percentage": "0" }, { "source": "Error", "percentage": "0" }, { "source": "Total", "percentage": "100" }]
		});
	}

	chart.updateProgress = function(elem, currentPct, errPct) {
		var selectedChart = $(elem).data("kendoChart");
		var ds = selectedChart.dataSource;
		var progress = ds.at(0);
		var error = ds.at(1);
		var total = ds.at(2);

		progress.set("percentage", currentPct);
		error.set("percentage", errPct);
		total.set("percentage", 100 - currentPct - errPct);

		selectedChart.refresh();
		this.setText(elem, currentPct + errPct);
		console.log("chart refreshed.");
	}

	chart.setText = function (elem, value) {
		if (value < 10)
			$(elem).append("<div id='" + $(this.chart).attr('id') + "-text' class='dcProgress-text dcProgress-text-1digit'>" + value + "<span class='dcProgress-text-pct'>%</span></div>");
		else if (value < 100)
			$(elem).append("<div id='" + $(this.chart).attr('id') + "-text' class='dcProgress-text dcProgress-text-2digit'>" + value + "<span class='dcProgress-text-pct'>%</span></div>");
		else
			$(elem).append("<div id='" + $(this.chart).attr('id') + "-text' class='dcProgress-text dcProgress-text-3digit'>" + value + "<span class='dcProgress-text-pct'>%</span></div>");
	}

	chart.testRealTimeUpdate = function (elem, interval, errModNumber) {
		var self = this;
		var updateInterval = (typeof interval === "undefined") ? 100 : interval;
		var selfTimer = setInterval(function () {
			var selectedChart = $(elem).data("kendoChart");
			var ds = selectedChart.dataSource;
			var progress = parseInt(ds.at(0).percentage);
			var error = parseInt(ds.at(1).percentage);
			var errMod = (typeof errModNumber === "undefined") ? 10 : errModNumber;
			
			
			progress += 1;
			if ((progress % errMod) === 0)
				error += 2;

			if (progress + error > 100) {
				progress = 100 - error;
				self.updateProgress(elem, progress, error);
				clearInterval(selfTimer);
			}
			else
				self.updateProgress(elem, progress, error);

			console.log("chart self update called with percentage: ", progress, " with error: ", error);
			
		}, updateInterval);
	};

	return chart;
})();