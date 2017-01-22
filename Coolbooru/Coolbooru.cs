using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Coolbooru {
	/// <summary>
	/// Represents a search query.
	/// </summary>
	public class CoolSearch {
		public List<CoolItem> search;
		public int total;
		public List<object> interactions; // I literally do not know what this does. Sorry
	}

	/// <summary>
	/// Represents an item in the Derpibooru database.
	/// </summary>
	public class CoolItem {
		public string id;
		public DateTime created_at;
		public DateTime updated_at;
		public List<object> duplicate_reports; // not sure what this does either
		public DateTime first_seen_at;
		public string uploader_id;
		public int score;
		public int comment_count;
		public int width;
		public int height;
		public string file_name;
		public string description;
		public string uploader;
		public string image;
		public int upvotes;
		public int downvotes;
		public int faves;
		public string tags;
		public List<string> tag_ids;
		public double aspect_ratio;
		public string original_format;
		public string mime_type;
		public string sha512_hash;
		public string orig_sha512_hash;
		public string source_url;
		public CoolRepresentation representations;
		public bool is_rendered;
		public bool is_optimized;
	}

	/// <summary>
	/// Represents a "Representation" (e.g. images) of the item
	/// </summary>
	public class CoolRepresentation {
		public string thumb_tiny;
		public string thumb_small;
		public string thumb;
		public string small;
		public string medium;
		public string full;
	}

	/// <summary>
	/// Represents the default Lists available.
	/// </summary>
	public class CoolLists {
		public List<CoolItem> top_scoring;
		public List<CoolItem> top_commented;
		public List<CoolItem> all_time_top_scoring;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents the result of the Images API call.
	/// </summary>
	public class CoolImages {
		public List<CoolItem> images;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents possible search result sorting modes.
	/// </summary>
	public class CoolSearchSort {
		public static string CreationDate { get { return "created_at"; } }
		public static string Score { get { return "score"; } }
		public static string Relevance { get { return "relevance"; } }
		public static string Wdith { get { return "width"; } }
		public static string Height { get { return "height"; } }
		public static string Random { get { return "random"; } }
	}

	/// <summary>
	/// Represents arguments to a search query.
	/// </summary>
	public class CoolSearchQuery {
		private string pq;
		private string psf;

		public string q {
			get { return pq; }
			set { pq = Uri.EscapeUriString(value); }
		}
		public string sf {
			get { return psf; }
			set { psf = Uri.EscapeUriString(value); }
		}
		public int page = 1;
		public bool comments = false;
		public bool fav = false;
	}

	/// <summary>
	/// Represents arguments to a list query.
	/// </summary>
	public class CoolListQuery {
		public string list;
		public int page = 1;
		public string last;
		public bool comments = false;
		public bool fav = false;
	}

	/// <summary>
	/// Represents a List.
	/// </summary>
	public class CoolList {
		public List<CoolItem> image;
		public List<object> interactions;
	}

	/// <summary>
	/// Represents a Gallery.
	/// </summary>
	public class CoolGallery {
		public DateTime created_at;
		public int creator_id;
		public string description;
		public int id;
		public int image_count;
		public string spoiler_warning;
		public string title;
		public DateTime updated_at;
		public int watcher_count;
	}

	/// <summary>
	/// Represents arguments to an front-page image list query.
	/// </summary>
	public class CoolImageQuery {
		public string constraint;
		public int page = 1;
		public int? gt;
		public int? gte;
		public int? lt;
		public int? lte;
		private char? pOrder = null;
		public char? order {
			get {
				return pOrder;
			}
			set {
				if( value != 'a' && value != 'd' && value != null) {
					throw new ArgumentException("Must be 'a' for ascending or 'd' for descending, or null");
				}
				pOrder = value;
			}
		}
		public bool deleted;
		public bool comments = false;
		public bool fav = false;
		public bool random_image;
	}

	/// <summary>
	/// Represents an OEmbed response.
	/// </summary>
	public class CoolEmbed {
		public string version;
		public string type;
		public string title;
		public string author_url;
		public string author_name;
		public string provider_name;
		public string provider_url;
		public int cache_age;
		public int derpibooru_id;
		public int derpibooru_score;
		public int derpibooru_comments;
		public List<string> derpibooru_tags;
		public string thumbnail_url;
	}

	/// <summary>
	/// The main functions of Coolbooru.
	/// </summary>
	public class Coolbooru {
		public const string CONSTRAINT_ID = "id";
		public const string CONSTRAINT_UPDATED = "updated";
		public const string CONSTRAINT_CREATED = "created";

		/// <summary>
		/// Creates a new HTTP client for use with API calls.
		/// </summary>
		/// <returns>a new HTTP client for use with API calls.</returns>
		private static HttpClient clientFactory() {
			HttpClient c = new HttpClient();
			c.DefaultRequestHeaders.UserAgent.ParseAdd("Coolbooru");
			return c;
		}

		/// <summary>
		/// Complete a response at url using the model T.
		/// </summary>
		/// <typeparam name="T">Model to serialize to.</typeparam>
		/// <param name="url">URL to query.</param>
		/// <returns>Response based on the model passed in.</returns>
		private static async Task<T> doResponse<T>(string url) {
			return JsonConvert.DeserializeObject<T>(await clientFactory().GetStringAsync(url));
		}

		/// <summary>
		/// Search Derpibooru using a certain tag or tags.
		/// See https://derpibooru.org/search/syntax for search syntax information.
		/// </summary>
		/// <param name="query">The tag(s) to search for (e.g. pinkie pie)</param>
		/// <param name="page">Pagination</param>
		/// <returns>A CoolSearch representing the result</returns>
		public static async Task<CoolSearch> search(string query, int page = 1) {
			return await doResponse<CoolSearch>("https://derpibooru.org/search.json?q=" + Uri.EscapeUriString(query) + "&page=" + page);
		}

		/// <summary>
		/// Search Derpibooru using a CoolSearchQuery.
		/// </summary>
		/// <param name="q">A CoolSearchQuery object</param>
		/// <returns>A CoolSearch representing the result</returns>
		public static async Task<CoolSearch> search(CoolSearchQuery q) {
			string url = "https://derpibooru.org/search.json?q=" + q.q + "&page=" + q.page;
			if (q.comments) url += "&comments=true";
			if (q.fav) url += "&fav=true";
			if (q.sf != null) url += "&sf=" + q.sf;

			return await doResponse<CoolSearch>(url);
		}

		/// <summary>
		/// Get information on a certain item
		/// </summary>
		/// <param name="itemID">An item ID.</param>
		/// <returns>A CoolItem representing the result</returns>
		public static async Task<CoolItem> item(int itemID) {
			return await doResponse<CoolItem>("https://derpibooru.org/" + itemID + ".json");
		}

		/// <summary>
		/// Get the contents of lists top_scoring, top_commented, all_time_top_scoring
		/// </summary>
		/// <returns>A CoolLists object representing the result</returns>
		public static async Task<CoolLists> lists() {
			return await doResponse<CoolLists>("https://derpibooru.org/lists.json");
		}

		/// <summary>
		/// Get the contents of a specific list.
		/// </summary>
		/// <param name="list">The list to get the contents of.</param>
		/// <returns>A CoolList object representing the result</returns>
		public static async Task<CoolList> list(string list, int page = 1) {
			return await doResponse<CoolList>("https://derpibooru.org/lists/" + list + ".json&page=" + page);
		}


		/// <summary>
		/// Get the contents of a specific list.
		/// </summary>
		/// <param name="q">A CoolListQuery representing the parameters of the request.</param>
		/// <returns>A CoolList object representing the result</returns>
		public static async Task<CoolList> list(CoolListQuery q) {
			string url = "https://derpibooru.org/lists/" + q.list + ".json?page=" + q.page;
			if (q.comments) url += "&comments=true";
			if (q.fav) url += "&fav=true";

			return await doResponse<CoolList>(url);
		}

		/// <summary>
		/// Get the front-page images.
		/// </summary>
		/// <returns>A CoolImages object representing the result</returns>
		public static async Task<CoolImages> images() {
			return await doResponse<CoolImages>("https://derpibooru.org/images.json");
		}

		/// <summary>
		/// Get a user's galleries.
		/// </summary>
		/// <param name="user">The user whose galleries you want to get.</param>
		/// <returns>A List of CoolGalleries representing the result</returns>
		public static async Task<List<CoolGallery>> userGalleries(string user) {
			return await doResponse<List<CoolGallery>>("https://derpibooru.org/galleries/" + user + ".json");
		}

		/// <summary>
		/// Get a user's galleries.
		/// </summary>
		/// <param name="user">The user whose galleries you want to get.</param>
		/// <param name="page">Pagination.</param>
		/// <param name="include_images">"When set, include arrays of image IDs featured in each gallery, in the order defined by the owning user, disregarding content filters." Doesn't seem to do much, though.</param>
		/// <returns>A List of CoolGalleries representing the result</returns>
		public static async Task<List<CoolGallery>> userGalleries(string user, int page = 1, bool include_images = false) {
			string url = "https://derpibooru.org/galleries/" + user + ".json?page=" + page;
			if (include_images) url += "&include_images=true";

			return await doResponse<List<CoolGallery>>(url);
		}

		/// <summary>
		/// Get a gallery.
		/// </summary>
		/// <param name="user">The user who owns the gallery.</param>
		/// <param name="id">The gallery ID.</param>
		/// <param name="page">Pagination.</param>
		/// <returns>A CoolGallery representing the result.</returns>
		public static async Task<CoolGallery> userGallery(string user, int id, int page = 1) {
			string url = "https://derpibooru.org/galleries/" + user + "/" + id + ".json?page=" + page;
			return await doResponse<CoolGallery>(url);
		}

		/// <summary>
		/// Get a gallery.
		/// </summary>
		/// <param name="id">The gallery ID.</param>
		/// <param name="page">Pagination.</param>
		/// <returns>A CoolGallery representing the result.</returns>
		public static async Task<CoolGallery> userGallery(int id, int page = 1) {
			return await doResponse<CoolGallery>("https://derpibooru.org/galleries/" + id + ".json?page=" + page);
		}

		/// <summary>
		/// Filter the front-page images.
		/// </summary>
		/// <param name="q">A CoolImageQuery representing the query.</param>
		/// <returns>A CoolImages object representing the result.</returns>
		public static async Task<CoolImages> images(CoolImageQuery q) {
			string url = "https://derpibooru.org/images.json?page=" + q.page;
			if (q.constraint != null) url += "&constraint=" + q.constraint;
			if (q.gt != null) url += "&gt=" + q.gt;
			if (q.gte != null) url += "&gte=" + q.gte;
			if (q.lt != null) url += "&lt=" + q.lt;
			if (q.lte != null) url += "&lte=" + q.lte;
			if (q.order != null) url += "&order=" + q.order;
			if (q.deleted) url += "&deleted=true";
			if (q.comments) url += "&comments=true";
			if (q.fav) url += "&fav=true";
			if (q.random_image) url += "&random_image=true";

			return await doResponse<CoolImages>(url);
		}

		/// <summary>
		/// Get OEmbed information from a post ID.
		/// </summary>
		/// <param name="id">A post ID.</param>
		/// <returns>A CoolEmbed object representing the result.</returns>
		public static async Task<CoolEmbed> embed(int id) {
			return await doResponse<CoolEmbed>("https://derpibooru.org/oembed.json?url=https://derpibooru.org/" + id);
		}

		/// <summary>
		/// Get OEmbed information from a URL.
		/// </summary>
		/// <param name="url">A URL.</param>
		/// <returns>A CoolEmbed object representing the result.</returns>
		public static async Task<CoolEmbed> embed(string url) {
			return await doResponse<CoolEmbed>("https://derpibooru.org/oembed.json?url=" + url);
		}
	}
}
