// Shared utilities & API helpers (ES module)
export const API_BASE = 'http://localhost:8080/api/v1';

export const $ = (sel, el = document) => el.querySelector(sel);
export const $$ = (sel, el = document) => Array.from(el.querySelectorAll(sel));

export const getQueryParam = (k) => new URLSearchParams(location.search).get(k);

function jsonHeaders() {
  return { 'Content-Type': 'application/json', 'Accept': 'application/json' };
}

export async function apiFetch(path, opts = {}) {
  const url = path.startsWith('http') ? path : `${API_BASE}${path}`;
  const init = { ...opts, headers: { ...(opts.headers || {}), ...jsonHeaders() } };
  const res = await fetch(url, init);
  const text = await res.text();
  let payload = null;
  try { payload = text ? JSON.parse(text) : null; } catch { payload = text; }
  if (!res.ok) {
    const msg = (payload && (payload.message || payload.error)) || `${res.status} ${res.statusText}`;
    const err = new Error(msg);
    err.status = res.status;
    err.payload = payload;
    throw err;
  }
  return payload;
}

export function renderStatus(el, kind, message) {
  if (!el) return;
  el.className = `status ${kind}`;
  el.textContent = message;
}

export function clearChildren(el) {
  //while (el.firstChild) el.removeChild(el.firstChild);
  el.replaceChildren();
}

export function formatGenres(genres) {
  if (!Array.isArray(genres) || genres.length === 0) return '—';
  return genres.map(g => `${g.id ?? '∅'}:${g.name ?? '—'}`).join(', ');
}

export async function fetchAllGenres() {
  try {
    const genres = await apiFetch('/genres'); // GET /api/v1/genres
    return genres.data ?? [];
  } catch (err) {
    return [];
  }
}

export function populateGenresSelect(selectEl, genres, selectedIds = []) {
  clearChildren(selectEl);
  for (const g of genres) {
    const opt = document.createElement('option');
    opt.value = String(g.id);
    opt.textContent = `${g.name}`;
    if (selectedIds.includes(g.id)) opt.selected = true;
    selectEl.appendChild(opt);
  }
}

export function captureMovieForm(form) {
  const title = form.title.value.trim();
  const year = Number(form.year.value);
  const rating = Number(form.rating.value);
  const description = form.description.value.trim();
  const selectedIds = Array.from(form.genres.selectedOptions).map(o => Number(o.value));
  const genres = selectedIds.map(id => ({ id }));
  return { title, year, rating, description, genres };
}
