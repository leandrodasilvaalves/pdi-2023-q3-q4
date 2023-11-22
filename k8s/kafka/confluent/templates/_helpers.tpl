{{/*
Expand the name of the chart.
*/}}
{{- define "kafka.fullname" -}}
{{- printf "%s-%s" .Release.Name .Chart.Name | trunc 63 | trimSuffix "-" }}
{{- end }}