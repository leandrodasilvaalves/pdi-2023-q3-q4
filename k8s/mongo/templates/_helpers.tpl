{{/*
Expand the name of the chart.
*/}}
{{- define "mongo.fullname" -}}
{{- if .Values.fullname -}}
{{- .Values.fullname | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name .Chart.Name | trunc 63 | trimSuffix "-" }}
{{- end -}}
{{- end -}}